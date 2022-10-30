using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BabyNames.Configuration;
using BabyNames.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BabyNames.Authentication;

public interface ITokenHandler
{
	string CreateToken(User user);
	TokenValidationResult ValidateToken(string tokenToValidate);
}

public class TokenHandler : ITokenHandler
{
	private readonly string _secretKey;
	private readonly string _audience;
	private readonly string _issuer;

	public TokenHandler(IOptions<JwtOptions> options)
	{
		_secretKey = options.Value.SecretKey ?? throw new ArgumentNullException(nameof(options));
		_audience = options.Value.Audience ?? throw new ArgumentNullException(nameof(options));
		_issuer = options.Value.Issuer ?? throw new ArgumentNullException(nameof(options));
	}

	public string CreateToken(User user)
	{
		var tokenHandler = new JwtSecurityTokenHandler();
		var signingKey = JwtSigningKeyFactory.GetSigningKey(_secretKey);
		var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new [] { new Claim("id", user.Id.ToString())}),
			Audience = _audience,
			Issuer = _issuer,
			IssuedAt = DateTime.UtcNow,
			SigningCredentials = signingCredentials
		};
		var token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}

	public TokenValidationResult ValidateToken(string tokenToValidate)
	{
		var tokenHandler = new JwtSecurityTokenHandler();

		try
		{
			tokenHandler.ValidateToken(tokenToValidate,
				new TokenValidationParameters
				{
					ValidateAudience = true,
					ValidAudience = _audience,
					ValidateIssuer = true,
					ValidIssuer = _issuer,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = JwtSigningKeyFactory.GetSigningKey(_secretKey)
				}, out var validatedToken);

			if (IsTokenExpired(validatedToken))
			{
				return new TokenValidationResult { ErrorMessage = "Token expired" };
			}

			return new TokenValidationResult { ValidatedToken = (JwtSecurityToken)validatedToken};
		}
		catch (Exception ex)
		{
			return new TokenValidationResult { ErrorMessage = ex.Message };
		}
	}

	private static bool IsTokenExpired(SecurityToken token)
	{
		var now = DateTime.UtcNow;
		return !(token.ValidFrom < now && now < token.ValidTo);
	}
}
