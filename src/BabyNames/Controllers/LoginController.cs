using BabyNames.Authentication;
using BabyNames.Configuration;
using BabyNames.Data;
using BabyNames.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BabyNames.Controllers;

public class LoginController : Controller
{
	private readonly IUserRepository _userRepository;
	private readonly ITokenHandler _tokenHandler;
	private readonly string _googleClientId;
	private readonly GoogleJsonWebSignature.ValidationSettings _validationSettings;

	public LoginController(IUserRepository userRepository, ITokenHandler tokenHandler, IOptions<GoogleAuthOptions> options)
	{
		_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
		_tokenHandler = tokenHandler ?? throw new ArgumentNullException(nameof(tokenHandler));
		var googleAuthOptions = options.Value;
		if (googleAuthOptions is null)
			throw new ArgumentNullException(nameof(options));
		_googleClientId = googleAuthOptions.ClientId ?? throw new ArgumentNullException(nameof(options));
		_validationSettings = new GoogleJsonWebSignature.ValidationSettings
		{
			Audience = new [] { _googleClientId }
		};
	}

	public IActionResult Prompt()
	{
		ViewData["ClientId"] = _googleClientId;
		return View("Login");
	}

	public IActionResult Logout()
	{
		if (Request.Cookies[AuthConstants.TokenCookieKey] is not null)
		{
			Response.Cookies.Delete(AuthConstants.TokenCookieKey, new CookieOptions
			{
				Secure = true,
				HttpOnly = true,
				SameSite = SameSiteMode.Strict
			});
		}

		return RedirectToAction("Prompt");
	}

	[HttpPost]
	public async Task<IActionResult> Complete([FromForm] GoogleAuthenticationResponse authenticationResponse)
	{
		var validatedToken = await GoogleJsonWebSignature.ValidateAsync(
			authenticationResponse.Credential,
			_validationSettings);
		if (validatedToken is null)
			return Unauthorized();

		var user = await GetOrCreateUser(validatedToken);
		if (user is null)
			return BadRequest();

		TempData[AuthConstants.LoginTokenKey] = _tokenHandler.CreateToken(user);
		return RedirectToAction("Index", "Home");
	}

	private async Task<User?> GetOrCreateUser(GoogleJsonWebSignature.Payload token)
	{
		var user = await _userRepository.GetUserByEmail(token.Email);
		if (user is not null)
			return user;

		await _userRepository.CreateUser(
			token.Email,
			token.Name,
			new Uri(token.Picture));
		return await _userRepository.GetUserByEmail(token.Email);
	}
}
