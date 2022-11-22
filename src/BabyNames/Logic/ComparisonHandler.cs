using BabyNames.Data;
using BabyNames.Models;

namespace BabyNames.Logic;

public interface IComparisonHandler
{
	Task<Result<ComparisonResult, string>> Compare(int sourceUserId, string targetUserEmail);
}

public class ComparisonHandler : IComparisonHandler
{
	private readonly IUserRepository _userRepository;
	private readonly IBabyNameRepository _babyNameRepository;

	public ComparisonHandler(IUserRepository userRepository, IBabyNameRepository babyNameRepository)
	{
		_userRepository = userRepository;
		_babyNameRepository = babyNameRepository;
	}

	public async Task<Result<ComparisonResult, string>> Compare(int sourceUserId, string targetUserEmail)
	{
		var sourceUser = await _userRepository.GetUser(sourceUserId);
		if (sourceUser is null)
			return Result<ComparisonResult, string>.Failure("Source user was not found");
		var targetUser = await _userRepository.GetUserByEmail(targetUserEmail);
		if (targetUser is null)
			return Result<ComparisonResult, string>.Failure("Target user was not found");
		var sourceUserNames = await GetChosenNames(sourceUserId);
		var targetUserNames = await GetChosenNames(targetUser.Id);
		var matches = sourceUserNames.Intersect(targetUserNames, new BabyNameComparerById());
		var result = new ComparisonResult(targetUser, matches);
		return Result<ComparisonResult, string>.Success(result);
	}

	private async Task<IEnumerable<BabyName>> GetChosenNames(int userId) =>
		(await _babyNameRepository.GetBabyNamesByUser(userId, null))
			.Where(name => name.Vote == Vote.Yea);

	private class BabyNameComparerById : IEqualityComparer<BabyName>
	{
		public bool Equals(BabyName? x, BabyName? y)
		{
			if (ReferenceEquals(x, y)) return true;
			if (ReferenceEquals(x, null)) return false;
			if (ReferenceEquals(y, null)) return false;
			if (x.GetType() != y.GetType()) return false;
			return x.Id == y.Id;
		}

		public int GetHashCode(BabyName obj)
		{
			return obj.Id;
		}
	}
}
