using Core.Application.Exceptions;
using Core.Application.Interfaces;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Rooms;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Web.Controllers.Admin
{
	public class RoomController : Controller
	{
		private readonly IRoomService _roomService;

		public RoomController(IRoomService pRoomService)
		{
			_roomService = pRoomService;
		}

		[HttpGet()]
		public async Task<IActionResult> Index([FromQuery] BaseListRQ pRequest)
		{
			ViewBag.List = await _roomService.List(pRequest);

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] RoomRQ pRequest)
		{
			try
			{
				await _roomService.Create(pRequest);
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
		public async Task<IActionResult> Update([FromBody] RoomRQ pRequest)
		{
			try
			{
				await _roomService.Update(pRequest);
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
		public async Task<IActionResult> Delete([FromQuery] int pId)
		{
			try
			{
				await _roomService.Delete(pId);
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, error = "Lỗi server: " + ex.Message });
			}
		}
	}
}
