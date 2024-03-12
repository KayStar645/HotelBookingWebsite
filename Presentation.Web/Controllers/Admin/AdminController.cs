using Microsoft.AspNetCore.Mvc;

namespace Presentation.Web.Controllers.Admin
{
	public class AdminController : Controller
	{
		public IActionResult Login()
		{
			return View();
		}
	}
}
