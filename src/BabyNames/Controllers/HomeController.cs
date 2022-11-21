using System.Diagnostics;
using BabyNames.Authentication;
using BabyNames.Models;
using Microsoft.AspNetCore.Mvc;

namespace BabyNames.Controllers;

public class HomeController : Controller
{
	private readonly ITokenHandler _tokenHandler;
	private string? CookieToken => Request.Cookies[AuthConstants.TokenCookieKey];
	private string? LoginToken => TempData[AuthConstants.LoginTokenKey]?.ToString();
	private bool IsLoggingIn => LoginToken != null;

	public HomeController(ITokenHandler tokenHandler)
	{
		_tokenHandler = tokenHandler ?? throw new ArgumentNullException(nameof(tokenHandler));
	}

	public IActionResult Index()
	{
		var token = IsLoggingIn ? LoginToken : CookieToken;

		if (!_tokenHandler.ValidateToken(token).WasSuccessful)
		{
			return RedirectToAction("Prompt", "Login");
		}

		if (IsLoggingIn)
		{
			Response.Cookies.Append(AuthConstants.TokenCookieKey, token!, new CookieOptions
			{
				Secure = true,
				HttpOnly = true,
				SameSite = SameSiteMode.Strict
			});
		}

		var model = new InitialData { PathBase = Request.PathBase };

		return View(model);

	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
