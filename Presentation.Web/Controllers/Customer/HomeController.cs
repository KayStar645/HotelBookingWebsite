using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Web.Controllers.Customer
{
    public class HomeController : Controller
    {

		[AllowAnonymous]
		public IActionResult Index()
        {
            return View();
        }
    }
}
