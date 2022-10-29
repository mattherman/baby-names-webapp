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
	private readonly AuthenticationOptions _authenticationOptions;
	private readonly GoogleJsonWebSignature.ValidationSettings _validationSettings;

	public LoginController(IUserRepository userRepository, IOptions<AuthenticationOptions> options)
	{
		_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
		_authenticationOptions = options.Value ?? throw new ArgumentNullException(nameof(options));
		_validationSettings = new GoogleJsonWebSignature.ValidationSettings
		{
			Audience = new [] { _authenticationOptions.GoogleClientId }
		};
	}

	public IActionResult Prompt()
	{
		ViewData["ClientId"] = _authenticationOptions.GoogleClientId;
		return View("Login");
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

		// TODO: Generate app-specific JWT for future authorization

		return RedirectToAction("Index", "Home");
	}

	private async Task<User?> GetOrCreateUser(GoogleJsonWebSignature.Payload token)
	{
		var user = await _userRepository.GetUser(token.Email);
		if (user is not null)
			return user;

		await _userRepository.CreateUser(
			token.Email,
			token.Name,
			new Uri(token.Picture));
		return await _userRepository.GetUser(token.Email);
	}
}
