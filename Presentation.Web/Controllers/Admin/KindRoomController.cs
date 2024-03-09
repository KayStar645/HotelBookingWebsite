﻿using Core.Application.Exceptions;
using Core.Application.Interfaces;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.KindRooms;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Web.Controllers.Admin
{
	public class KindRoomController : Controller
	{
		private readonly IKindRoomService _kindRoomService;

		public KindRoomController(IKindRoomService pKindRoomService)
		{
			_kindRoomService = pKindRoomService;
		}

		[HttpGet()]
		public async Task<IActionResult> Index([FromQuery] BaseListRQ pRequest)
		{
			ViewBag.List = await _kindRoomService.List(pRequest);

			return View();
		}

		[HttpGet("/kindroom/detail")]
		public async Task<IActionResult> Detail([FromQuery] int pId)
		{
			ViewBag.Detail = await _kindRoomService.Detail(pId);

			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Create([FromBody] KindRoomRQ pRequest)
		{
			try
			{
				await _kindRoomService.Create(pRequest);
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
		public async Task<IActionResult> Update([FromBody] KindRoomRQ pRequest)
		{
			try
			{
				await _kindRoomService.Update(pRequest);
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
				await _kindRoomService.Delete(pId);
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, error = "Lỗi server: " + ex.Message });
			}
		}
	}
}
