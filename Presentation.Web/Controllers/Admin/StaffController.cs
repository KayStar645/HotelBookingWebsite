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

        public async Task<IActionResult> Index()
        {
            var rq = new BaseListRQ();
            var list = await _staffService.List(rq);

            ViewBag.List = list;

            return View();
        }

        public async Task<IActionResult> Detail(int pId)
        {

			return View();
		}

        [HttpPost]
		public async Task<IActionResult> Create([FromBody]StaffRQ pRequest)
		{
            await _staffService.Create(pRequest);
			return RedirectToAction("index");
		}

        [HttpPut]
		public async Task<IActionResult> Update([FromBody]StaffRQ pRequest)
		{
            await _staffService.Update(pRequest);
			return RedirectToAction("index");
		}

        public async Task<IActionResult> Delete(int pId)
		{
            await _staffService.Delete(pId);
			return RedirectToAction("index");
		}
	}
}
