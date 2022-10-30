using System.IdentityModel.Tokens.Jwt;

namespace BabyNames.Authentication;

public class TokenValidationResult
{
	public bool WasSuccessful => ValidatedToken != null;
	public JwtSecurityToken? ValidatedToken { get; set; }
	public string ErrorMessage { get; set; } = "";
}
