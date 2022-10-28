using BabyNames.Data.Queries;
using BabyNames.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

namespace BabyNames.Data;

public interface IUserRepository
{
	Task<User?> GetUser(string emailAddress);
	Task CreateUser(string emailAddress, string fullName, Uri pictureUri);
}

public class UserRepository : IUserRepository
{
	private readonly string _connectionString;

	public UserRepository(IOptions<DatabaseOptions> databaseOptions)
	{
		_connectionString = $"Data Source={databaseOptions.Value.DatabaseFile};";
	}

	public async Task<User?> GetUser(string emailAddress)
	{
		await using var connection = new SqliteConnection(_connectionString);
		await connection.OpenAsync();
		return await connection.QueryFirstOrDefaultAsync<User>(
			Query.GetUserByEmail,
			new { Email = emailAddress });
	}

	public async Task CreateUser(string emailAddress, string fullName, Uri pictureUri)
	{
		await using var connection = new SqliteConnection(_connectionString);
		await connection.OpenAsync();
		await connection.ExecuteAsync(
			Query.CreateUser,
			new { Email = emailAddress, Name = fullName, PictureUri = pictureUri.ToString() });
	}
}
