using Microsoft.AspNetCore.Mvc;

namespace Presentation.Web.Controllers.Auth
{
    public class PermissionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
