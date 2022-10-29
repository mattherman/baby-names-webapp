using System.Diagnostics;
using BabyNames.Models;
using Microsoft.AspNetCore.Mvc;

namespace BabyNames.Controllers;

public class HomeController : Controller
{
	public IActionResult Index()
	{
		var tokenFromLogin = TempData["Token"];
		if (tokenFromLogin is not null)
		{
			Response.Cookies.Append("__Host-id", (string)tokenFromLogin, new CookieOptions
			{
				Secure = true,
				HttpOnly = true,
				SameSite = SameSiteMode.Strict,
				Path = "/"
			});
		}
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
