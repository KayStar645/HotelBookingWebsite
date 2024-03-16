using Core.Application.Exceptions;
using Core.Application.Interfaces.Auth;
using Core.Application.ViewModels.Auth;
using Core.Application.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;
using Presentation.Web.Middleware;

namespace Presentation.Web.Controllers.Auth
{
	public class RoleController : Controller
	{
		private readonly IRoleService _roleService;
		private readonly IPermissionService _permissionService;

		public RoleController(IRoleService pRoleService, IPermissionService pPermissionService)
		{
			_roleService = pRoleService;
			_permissionService = pPermissionService;
		}

		[HttpGet]
		[Permission("role-view")]
		public async Task<IActionResult> Index([FromQuery] BaseListRQ pRequest)
		{
			ViewBag.List = await _roleService.List(pRequest);
			ViewBag.Detail = await _permissionService.PermissionByRole();

			return View();
		}

		[HttpGet("/role/detail")]
		[Permission("role-view")]
		public async Task<IActionResult> Detail([FromQuery] int pId)
		{
			ViewBag.Detail = await _permissionService.PermissionByRole(pId);

			return View();
		}



		[HttpPost]
		[Permission("role-create")]
		public async Task<IActionResult> Create([FromBody] RoleRQ pRequest)
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

				await _roleService.CreateAsync(pRequest);
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
