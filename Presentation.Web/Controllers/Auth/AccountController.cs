using Microsoft.AspNetCore.Mvc;

namespace Presentation.Web.Controllers.Auth
{
	public class AccountController : Controller
	{
		public IActionResult Login()
		{
			return View();
		}
	}
}
