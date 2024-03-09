using Core.Application.Exceptions;
using Core.Application.Interfaces;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Staffs;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Web.Controllers.Admin
{
	public class StaffController : Controller
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService pStaffService)
        {
            _staffService = pStaffService;
        }

		[HttpGet()]
        public async Task<IActionResult> Index([FromQuery] BaseListRQ pRequest)
        {
			ViewBag.List = await _staffService.List(pRequest);

            return View();
        }

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] StaffRQ pRequest)
		{
			try
			{
				await _staffService.Create(pRequest);
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
		public async Task<IActionResult> Update([FromBody]StaffRQ pRequest)
		{
			try
			{
				await _staffService.Update(pRequest);
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
                await _staffService.Delete(pId);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Lỗi server: " + ex.Message });
            }
		}
	}
}
