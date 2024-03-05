using Microsoft.AspNetCore.Mvc;

namespace Presentation.Web.Controllers.Customer
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
