namespace BabyNames.Models;

public class BabyName
{
	private BabyName() { }

	public BabyName(int id, string name, NameGender gender, Vote? vote = null)
	{
		Id = id;
		Name = name;
		Gender = gender;
		Vote = vote;
	}

	public int Id { get; }
	public string Name { get; } = "";
	public NameGender Gender { get; }
	public Vote? Vote { get; set; }
}
