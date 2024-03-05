using Microsoft.AspNetCore.Mvc;

namespace Presentation.Web.Controllers.Admin
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Chào bạn!";
            return View();
        }
    }
}
