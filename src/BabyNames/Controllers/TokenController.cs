using BabyNames.Authentication;
using BabyNames.Data;
using BabyNames.Models;
using Microsoft.AspNetCore.Mvc;

namespace BabyNames.Controllers;

[ApiController]
[Route("/api/token")]
public class TokenController : ControllerBase
{
	private readonly ITokenHandler _tokenHandler;
	private readonly IUserRepository _userRepository;

	public TokenController(ITokenHandler tokenHandler, IUserRepository userRepository)
	{
		_tokenHandler = tokenHandler ?? throw new ArgumentNullException(nameof(tokenHandler));
		_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
	}

	[HttpGet]
	public IActionResult GetToken()
	{
		var token = Request.Cookies[AuthConstants.TokenCookieKey];
		if (token is not null)
		{
			return Ok(new TokenResponse(token));
		}

		return Unauthorized();
	}

	[HttpPost]
	[Route("refresh")]
	public async Task<IActionResult> RefreshToken()
	{
		var token = Request.Cookies[AuthConstants.TokenCookieKey];
		if (token is null)
			return Unauthorized("Token cookie was not present");

		var tokenValidationResult = _tokenHandler.ValidateToken(token);
		if (!tokenValidationResult.WasSuccessful)
			return Unauthorized("Token was not valid");

		var userIdClaim = tokenValidationResult.ValidatedToken?.Claims.FirstOrDefault(c => c.Type == "id");
		var hasValidUserId = int.TryParse(userIdClaim?.Value, out var userId);
		if (!hasValidUserId)
			return Unauthorized("Token did not contain a valid user id");

		var user = await _userRepository.GetUser(userId);
		if (user is null)
			return Unauthorized("User does not exist");

		var newToken = _tokenHandler.CreateToken(user);

		Response.Cookies.Append(AuthConstants.TokenCookieKey, newToken, new CookieOptions
		{
			Secure = true,
			HttpOnly = true,
			SameSite = SameSiteMode.Strict
		});

		return Ok(new TokenResponse(newToken));
	}
}
