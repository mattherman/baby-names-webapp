using BabyNames.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using BabyNames.Data.Queries;
using Microsoft.Extensions.Options;

namespace BabyNames.Data;

public interface IBabyNameRepository
{
	Task<IEnumerable<BabyName>> GetBabyNamesByUser(NameGender? gender);
	Task<IEnumerable<BabyName>> GetBabyNamesByUserPendingVote(NameGender? gender);
	Task<BabyName?> GetBabyName(int id);
	Task Vote(int id, Vote vote);
}

public class BabyNameRepository : IBabyNameRepository
{
	private readonly string _connectionString;

	public BabyNameRepository(IOptions<DatabaseOptions> databaseOptions)
	{
		_connectionString = $"Data Source={databaseOptions.Value.DatabaseFile};";
	}

	public async Task<IEnumerable<BabyName>> GetBabyNamesByUser(NameGender? gender)
	{
		await using var connection = new SqliteConnection(_connectionString);
		await connection.OpenAsync();
		var results = await connection.QueryAsync<BabyName>(
			Query.GetBabyNamesByUser,
			new { UserId = 1, Gender = gender?.ToString() });
		return results ?? Enumerable.Empty<BabyName>();
	}

	public async Task<IEnumerable<BabyName>> GetBabyNamesByUserPendingVote(NameGender? gender)
	{
		await using var connection = new SqliteConnection(_connectionString);
		await connection.OpenAsync();
		var results = await connection.QueryAsync<BabyName>(
			Query.GetBabyNamesByUserPendingVote,
			new { UserId = 1, Gender = gender?.ToString() });
		return results ?? Enumerable.Empty<BabyName>();
	}

	public async Task<BabyName?> GetBabyName(int id)
	{
		await using var connection = new SqliteConnection(_connectionString);
		await connection.OpenAsync();
		return await connection.QueryFirstOrDefaultAsync<BabyName>(
			Query.GetBabyName,
			new { UserId = 1, NameId = id });
	}

	public async Task Vote(int id, Vote vote)
	{
		await using var connection = new SqliteConnection(_connectionString);
		await connection.OpenAsync();
		await connection.ExecuteAsync(
			Query.CastVote,
			new { UserId = 1, NameId = id, Vote = (int)vote });
	}
}
