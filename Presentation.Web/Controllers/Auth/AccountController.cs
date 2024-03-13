using Core.Application.Common;
using Core.Application.Exceptions;
using Core.Application.Interfaces.Auth;
using Core.Application.ViewModels.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Web.Controllers.Auth
{
	public class AccountController : Controller
	{
		private readonly IAccountService _accountService;

		public AccountController(IAccountService pAccountService)
		{
			_accountService = pAccountService;
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromBody] LoginRQ pRequest)
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

				var result = await _accountService.LoginAsync(pRequest);
				return Json(new { success = true , data = result});
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
