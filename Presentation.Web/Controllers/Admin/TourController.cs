using Core.Application.Exceptions;
using Core.Application.Interfaces;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Tour;
using Microsoft.AspNetCore.Mvc;
using Presentation.Web.Middleware;

namespace Presentation.Web.Controllers.Admin
{
    public class TourController : Controller
    { 
        private readonly ITourService _tourService;

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }
        [HttpGet]
        [Permission("tour-view")]
        public async Task<IActionResult> Index([FromQuery] BaseListRQ pRequest)
        {
            
            ViewBag.List = await _tourService.List(pRequest);
            return View();
        }


        [HttpPost]
        [Permission("tour-create")]
        public async Task<IActionResult> Create([FromBody] TourRQ pRequest)
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

                await _tourService.Create(pRequest);
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
        [Permission("tour-update")]
        public async Task<IActionResult> Update([FromBody] TourRQ pRequest)
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

                await _tourService.Update(pRequest);
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
        [Permission("tour-delete")]
        public async Task<IActionResult> Delete([FromQuery] int pId)
        {
            try
            {
                await _tourService.Delete(pId);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Lỗi server: " + ex.Message });
            }
        }
    }
}
