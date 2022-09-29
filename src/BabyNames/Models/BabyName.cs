namespace BabyNames.Models;

public class BabyName
{
	public BabyName(int id, string name, NameGender gender)
	{
		Id = id;
		Name = name;
		Gender = gender;
	}

	public int Id { get; }
	public string Name { get; }
	public NameGender Gender { get; }
}
