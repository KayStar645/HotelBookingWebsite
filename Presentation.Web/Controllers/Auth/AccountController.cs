﻿using Core.Application.Common;
using Core.Application.Exceptions;
using Core.Application.Interfaces.Auth;
using Core.Application.ViewModels.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

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
		[AllowAnonymous]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
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

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(result.Token) as JwtSecurityToken;

                Response.Cookies.Append("Token", "Bearer " + result.Token, new CookieOptions
				{
					Expires = jwtToken.ValidTo,
					Secure = true,
					HttpOnly = true
				});

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

		[HttpGet]
		public IActionResult Logout()
		{
			Response.Cookies.Delete("Token");

			return RedirectToAction("Login");
		}

	}
}
