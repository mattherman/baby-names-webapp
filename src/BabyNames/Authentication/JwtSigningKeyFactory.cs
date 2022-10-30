using Microsoft.IdentityModel.Tokens;

namespace BabyNames.Authentication;

public static class JwtSigningKeyFactory
{
	public static SymmetricSecurityKey GetSigningKey(string base64EncodedSecretKey)
	{
		return new SymmetricSecurityKey(Convert.FromBase64String(base64EncodedSecretKey));
	}
}
