using Microsoft.AspNetCore.Mvc;
using Presentation.Web.Middleware;

namespace Presentation.Web.Controllers.Admin
{
    public class DashboardController : Controller
    {
		[Permission("dashboard-view")]
		public IActionResult Index()
        {
            ViewData["Title"] = "Chào bạn!";
            return View();
        }
    }
}
