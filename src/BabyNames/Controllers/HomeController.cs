using System.Diagnostics;
using BabyNames.Authentication;
using BabyNames.Models;
using Microsoft.AspNetCore.Mvc;

namespace BabyNames.Controllers;

public class HomeController : Controller
{
	private readonly ITokenHandler _tokenHandler;
	private string? LoginToken => TempData["Token"]?.ToString();
	private bool IsCompletingLogin => LoginToken != null;
	private string? CookieToken => Request.Cookies[CookieKeys.TokenCookieKey];
	private bool IsAlreadyLoggedIn => Request.Cookies.ContainsKey(CookieKeys.TokenCookieKey);

	public HomeController(ITokenHandler tokenHandler)
	{
		_tokenHandler = tokenHandler ?? throw new ArgumentNullException(nameof(tokenHandler));
	}

	public IActionResult Index()
	{
		if (IsCompletingLogin)
		{
			Response.Cookies.Append(CookieKeys.TokenCookieKey, LoginToken!, new CookieOptions
			{
				Secure = true,
				HttpOnly = true,
				SameSite = SameSiteMode.Strict,
				Path = ""
			});
			return View();
		}

		if (IsAlreadyLoggedIn && _tokenHandler.ValidateToken(CookieToken!).WasSuccessful)
		{
			return View();
		}

		return RedirectToAction("Prompt", "Login");
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
