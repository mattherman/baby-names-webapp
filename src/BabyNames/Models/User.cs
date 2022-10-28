namespace BabyNames.Models;

public class User
{
	public int Id { get; set; }
	public string EmailAddress { get; set; } = "";
	public string FullName { get; set; } = "";
	public Uri? PictureUri { get; set; }
}
