using Core.Application.Interfaces.Auth;
using Core.Application.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Web.Controllers.Auth
{
	public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService pUserService)
        {
            _userService = pUserService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] BaseListRQ pRequest)
        {
            ViewBag.List = await _userService.List(pRequest);

            return View();
        }
    }
}
