using Core.Application.Exceptions;
using Core.Application.Interfaces.Auth;
using Core.Application.ViewModels.Auth;
using Core.Application.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;
using Presentation.Web.Middleware;

namespace Presentation.Web.Controllers.Auth
{
	public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(IUserService pUserService, IRoleService pRoleService)
        {
            _userService = pUserService;
            _roleService = pRoleService;
        }

        [HttpGet]
		[Permission("user-view")]
		public async Task<IActionResult> Index([FromQuery] BaseListRQ pRequest)
        {
            ViewBag.List = await _userService.List(pRequest);
			ViewBag.Roles = await _roleService.List(pRequest);

			return View();
        }

		[HttpPost]
		[Permission("user-create")]
		public async Task<IActionResult> Create([FromBody] UserRQ pRequest)
		{
			try
			{
				var modelStateErrors = ModelState.Values
					.SelectMany(v => v.Errors)
					.Select(e => e.ErrorMessage)
					.ToList();
				if (modelStateErrors.Any())
				{
					return Json(new { success = false, errors = modelStateErrors });
				}

				await _userService.Create(pRequest);
				return Json(new { success = true });
			}
			catch (ValidationCustomException ex)
			{
				return Json(new { success = false, errors = ex.Errors });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, error = "Lỗi server: " + ex.Message });
			}
		}


		[HttpPut]
		[Permission("user-update")]
		public async Task<IActionResult> Update([FromBody] UserRQ pRequest)
		{
			try
			{
				var modelStateErrors = ModelState.Values
					.SelectMany(v => v.Errors)
					.Select(e => e.ErrorMessage)
					.ToList();
				if (modelStateErrors.Any())
				{
					return Json(new { success = false, errors = modelStateErrors });
				}

				await _userService.Update(pRequest);
				return Json(new { success = true });
			}
			catch (ValidationCustomException ex)
			{
				return Json(new { success = false, errors = ex.Errors });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, error = "Lỗi server: " + ex.Message });
			}
		}
	}
}
