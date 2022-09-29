using BabyNames.Models;

namespace BabyNames.Data;

public interface IBabyNameRepository
{
	Task<IEnumerable<BabyName>> GetBabyNames(IEnumerable<NameGender> genders);
}

public class BabyNameRepository : IBabyNameRepository
{
	private IEnumerable<BabyName> _names = new[]
	{
		new BabyName(1, "Matthew", NameGender.Male), new BabyName(2, "Ashtyn", NameGender.Female),
		new BabyName(3, "Sam", NameGender.Unisex),
	};

	public async Task<IEnumerable<BabyName>> GetBabyNames(IEnumerable<NameGender> genders)
	{
		var result = _names.Where(name => genders.Contains(name.Gender));
		return await Task.FromResult(result);
	}
}
