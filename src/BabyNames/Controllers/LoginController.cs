using BabyNames.Data;
using BabyNames.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;

namespace BabyNames.Controllers;

public class LoginController : Controller
{
	private readonly IUserRepository _userRepository;

	public LoginController(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public IActionResult Prompt()
	{
		return View("Login");
	}

	[HttpPost]
	public async Task<IActionResult> Complete([FromForm] GoogleAuthenticationResponse authenticationResponse)
	{
		var validatedToken = await GoogleJsonWebSignature.ValidateAsync(authenticationResponse.Credential);
		if (validatedToken is null)
			return Unauthorized();

		var user = await _userRepository.GetUser(validatedToken.Email);
		if (user is null)
		{
			user = new User
			{
				Name = validatedToken.Name,
				EmailAddress = validatedToken.Email,
				PictureUri = new Uri(validatedToken.Picture)
			};
			await _userRepository.CreateUser(user);
		}
		// TODO: Generate app-specific JWT for future authorization
		return RedirectToAction("Index", "Home");
	}
}
