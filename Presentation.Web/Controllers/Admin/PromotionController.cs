using Core.Application.Exceptions;
using Core.Application.Interfaces;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Promotions;
using Microsoft.AspNetCore.Mvc;
using Presentation.Web.Middleware;

namespace Presentation.Web.Controllers.Admin
{
	public class PromotionController : Controller
	{
		private readonly IPromotionService _promotionService;

		public PromotionController(IPromotionService pPromotionService)
		{
			_promotionService = pPromotionService;
		}

		[HttpGet]
		[Permission("promotion-view")]
		public async Task<IActionResult> Index([FromQuery] BaseListRQ pRequest)
		{
			ViewBag.List = await _promotionService.List(pRequest);

			return View();
		}

		[HttpGet("/promotion/detail")]
		[Permission("promotion-view")]
		public async Task<IActionResult> Detail([FromQuery] int pId)
		{
			ViewBag.Detail = await _promotionService.Detail(pId);

			return View();
		}


		[HttpPost]
		[Permission("promotion-create")]
		public async Task<IActionResult> Create([FromBody] PromotionRQ pRequest)
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

				await _promotionService.Create(pRequest);
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
		[Permission("promotion-update")]
		public async Task<IActionResult> Update([FromBody] PromotionRQ pRequest)
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

				await _promotionService.Update(pRequest);
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
		[Permission("promotion-delete")]
		public async Task<IActionResult> Delete([FromQuery] int pId)
		{
			try
			{
				await _promotionService.Delete(pId);
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, error = "Lỗi server: " + ex.Message });
			}
		}

		[HttpPut]
		[Permission("promotion-approve")]
		public async Task<IActionResult> Approve([FromQuery] int pId, string pStatus)
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

				var result = await _promotionService.Approve(pId, pStatus);
				return Json(new { success = result });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, error = "Trạng thái không hợp lệ!" });
			}
		}
	}
}
