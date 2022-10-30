using BabyNames.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace BabyNames.Tests;

public class TestData
{
	private readonly string _connectionString;
	public IDictionary<string, int> BabyNameIds = new Dictionary<string, int>();
	public IDictionary<string, User> Users = new Dictionary<string, User>();

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
		Users = new Dictionary<string, User>();
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

	public async Task CreateUser(string name)
	{
		await using var connection = new SqliteConnection(_connectionString);
		await connection.OpenAsync();
		await connection.ExecuteAsync(
			"INSERT INTO Users (EmailAddress, FullName) VALUES (@Email, @Name);",
			new { Email = "", Name = name });
		var id = await connection.QuerySingleAsync<int>(
			"SELECT Id FROM Users WHERE FullName = @Name",
			new { Name = name });
		var user = new User { Id = id, FullName = name };
		Users.Add(name, user);
	}
}
