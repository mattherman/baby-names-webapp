using BabyNames.Models;

namespace BabyNames.Data;

public interface IBabyNameRepository
{
	Task<IEnumerable<BabyName>> GetBabyNames(NameGender? gender);
	Task<IEnumerable<BabyName>> GetBabyNamesPendingVote(NameGender? gender);
	Task<BabyName?> GetBabyName(int id);
	Task Vote(int id, Vote vote);
}

public class BabyNameRepository : IBabyNameRepository
{
	private readonly IEnumerable<BabyName> _names = new[]
	{
		new BabyName(1, "Matthew", NameGender.Male),
		new BabyName(2, "Ashtyn", NameGender.Female),
		new BabyName(3, "Sam", NameGender.Male),
		new BabyName(4, "Sam", NameGender.Female),
	};

	public async Task<IEnumerable<BabyName>> GetBabyNames(NameGender? gender)
	{
		var result =
			gender.HasValue ? _names.Where(name => name.Gender == gender) : _names;
		return await Task.FromResult(result);
	}

	public async Task<IEnumerable<BabyName>> GetBabyNamesPendingVote(NameGender? gender)
	{
		var matchingGender =
			gender.HasValue ? _names.Where(name => name.Gender == gender) : _names;
		var pendingVote = matchingGender.Where(name => name.Vote is null);
		return await Task.FromResult(pendingVote);
	}

	public async Task<BabyName?> GetBabyName(int id)
	{
		var result = _names.FirstOrDefault(name => name.Id == id);
		return await Task.FromResult(result);
	}

	public async Task Vote(int id, Vote vote)
	{
		var name = _names.FirstOrDefault(name => name.Id == id);
		if (name is null) return;
		name.Vote = vote;
		await Task.CompletedTask;
	}
}
