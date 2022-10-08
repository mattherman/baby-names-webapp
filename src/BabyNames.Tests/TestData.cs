using BabyNames.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace BabyNames.Tests;

public class TestData
{
	private readonly string _connectionString;
	public IDictionary<string, int> BabyNameIds = new Dictionary<string, int>();
	public IDictionary<string, int> UserIds = new Dictionary<string, int>();

	public TestData()
	{
		var databaseFile = Environment.GetEnvironmentVariable("DATABASE__DATABASEFILE");
		if (databaseFile is null)
			throw new ArgumentNullException(nameof(databaseFile));
		_connectionString = $"Data Source={databaseFile};";
	}

	public async Task ResetDatabase()
	{
		await using var connection = new SqliteConnection(_connectionString);
		await connection.OpenAsync();
		await connection.ExecuteAsync("DELETE FROM UserVotes;");
		await connection.ExecuteAsync("DELETE FROM Users;");
		await connection.ExecuteAsync("DELETE FROM BabyNames;");
		BabyNameIds = new Dictionary<string, int>();
		UserIds = new Dictionary<string, int>();
	}

	public async Task<int> CreateBabyName(string name, NameGender gender)
	{
		await using var connection = new SqliteConnection(_connectionString);
		await connection.OpenAsync();
		await connection.ExecuteAsync(
			"INSERT INTO BabyNames (Name, Gender) VALUES (@Name, @Gender);",
			new { Name = name, Gender = gender.ToString() });
		var id = await connection.QuerySingleAsync<int>(
			"SELECT Id FROM BabyNames WHERE Name = @Name AND Gender = @Gender;",
			new { Name = name, Gender = gender.ToString() });
		BabyNameIds.Add(name, id);
		return id;
	}

	public async Task<int> CreateUser(string username)
	{
		await using var connection = new SqliteConnection(_connectionString);
		await connection.OpenAsync();
		await connection.ExecuteAsync(
			"INSERT INTO Users (Username) VALUES (@Username);",
			new { Username = username });
		var id = await connection.QuerySingleAsync<int>(
			"SELECT Id FROM Users WHERE Username = @Username",
			new { Username = username });
		UserIds.Add(username, id);
		return id;
	}
}
