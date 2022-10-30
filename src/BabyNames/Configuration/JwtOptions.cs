namespace BabyNames.Configuration;

public class JwtOptions
{
	public string? SecretKey { get; set; }
	public string? Audience { get; set; }
	public string? Issuer { get; set; }
}