namespace BabyNames.Models;

public class ComparisonResult
{
	public ComparisonResult(User comparedTo, IEnumerable<BabyName> matches)
	{
		ComparedTo = comparedTo;
		Matches = matches;
	}

	public User ComparedTo { get; }
	public IEnumerable<BabyName> Matches { get; set; }
}
