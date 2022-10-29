using System.IdentityModel.Tokens.Jwt;
using BabyNames.Configuration;
using BabyNames.Data;
using BabyNames.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BabyNames.Controllers;

public class LoginController : Controller
{
	private readonly IUserRepository _userRepository;
	private readonly string _googleClientId;
	private readonly string _jwtSecretKey;
	private readonly GoogleJsonWebSignature.ValidationSettings _validationSettings;

	public LoginController(IUserRepository userRepository, IOptions<AuthenticationOptions> options)
	{
		_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
		var authenticationOptions = options.Value;
		if (authenticationOptions is null)
			throw new ArgumentNullException(nameof(options));
		_googleClientId = authenticationOptions.GoogleClientId ?? throw new ArgumentNullException(nameof(options));
		_jwtSecretKey = authenticationOptions.SecretKey ?? throw new ArgumentNullException(nameof(options));
		_validationSettings = new GoogleJsonWebSignature.ValidationSettings
		{
			Audience = new [] { _googleClientId }
		};
	}

	private SymmetricSecurityKey JwtSigningKey => new(Convert.FromBase64String(_jwtSecretKey));

	public IActionResult Prompt()
	{
		ViewData["ClientId"] = _googleClientId;
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

		var tokenHandler = new JwtSecurityTokenHandler();
		var signingCredentials = new SigningCredentials(JwtSigningKey, SecurityAlgorithms.HmacSha256Signature);
		var tokenDescriptor = new SecurityTokenDescriptor { SigningCredentials = signingCredentials };
		var token = tokenHandler.CreateToken(tokenDescriptor);
		var tokenString = tokenHandler.WriteToken(token);

		TempData["Token"] = tokenString;
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
