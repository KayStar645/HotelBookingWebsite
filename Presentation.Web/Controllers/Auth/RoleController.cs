using Microsoft.AspNetCore.Mvc;

namespace Presentation.Web.Controllers.Auth
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
