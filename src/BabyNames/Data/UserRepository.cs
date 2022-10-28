using BabyNames.Models;
using Microsoft.Extensions.Options;

namespace BabyNames.Data;

public interface IUserRepository
{
	Task<User?> GetUser(string emailAddress);
	Task CreateUser(User user);
}
public class UserRepository : IUserRepository
{
	private readonly string _connectionString;

	public UserRepository(IOptions<DatabaseOptions> databaseOptions)
	{
		_connectionString = $"Data Source={databaseOptions.Value.DatabaseFile};";
	}

	public Task<User> GetUser(string emailAddress)
	{
		throw new NotImplementedException();
	}

	public Task CreateUser(User user)
	{
		throw new NotImplementedException();
	}
}
