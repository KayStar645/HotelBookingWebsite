using Core.Application.Exceptions;
using Core.Application.Interfaces;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Web.Middleware;

namespace Presentation.Web.Controllers.Admin
{
	public class ServiceController : Controller
	{
		private readonly IServiceService _serviceService;

		public ServiceController(IServiceService serviceService)
		{
			_serviceService = serviceService;
		}

		[HttpGet]
        [Permission("service-view")]
        public async Task<IActionResult> Index([FromQuery] BaseListRQ pRequest)
		{
			ViewBag.List = await _serviceService.List(pRequest);

			return View();
		}

		[HttpPost]
        [Permission("service-create")]
        public async Task<IActionResult> Create([FromBody] ServiceRQ pRequest)
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

                await _serviceService.Create(pRequest);
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
        [Permission("service-update")]
        public async Task<IActionResult> Update([FromBody] ServiceRQ pRequest)
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

                await _serviceService.Update(pRequest);
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

		[HttpDelete]
        [Permission("service-delete")]
        public async Task<IActionResult> Delete([FromQuery] int pId)
		{
			try
			{
				await _serviceService.Delete(pId);
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, error = "Lỗi server: " + ex.Message });
			}
		}
	}
}
