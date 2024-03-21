using Core.Application.Exceptions;
using Core.Application.Interfaces;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Booking;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Web.Middleware;
using Core.Application.Services;

namespace Presentation.Web.Controllers.Admin
{
	public class BookingController : Controller
	{
		private readonly IBookingService _bookingService;
		private readonly ITourService _tourService;

		public BookingController(IBookingService pBookingService, ITourService pTourService)
		{
			_bookingService = pBookingService;
			_tourService = pTourService;
		}

		[HttpGet]
		[Permission("booking-view")]
		public async Task<IActionResult> Index([FromQuery] BaseListRQ pRequest)
		{
			ViewBag.List = await _bookingService.List(pRequest);
			var tourRQ = new BaseListRQ
			{
				Page = 1,
				PageSize = -1,
			};
			ViewBag.Tours = await _tourService.List(tourRQ);

			return View();
		}


		[HttpPost]
		[Permission("booking-create")]
		public async Task<IActionResult> Create([FromBody] BookingRQ pRequest)
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

				await _bookingService.Create(pRequest);
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
		[Permission("booking-update")]
		public async Task<IActionResult> Update([FromBody] BookingRQ pRequest)
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

				await _bookingService.Update(pRequest);
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
		[Permission("booking-delete")]
		public async Task<IActionResult> Delete([FromQuery] int pId)
		{
			try
			{
				await _bookingService.Delete(pId);
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, error = "Lỗi server: " + ex.Message });
			}
		}
	}
}