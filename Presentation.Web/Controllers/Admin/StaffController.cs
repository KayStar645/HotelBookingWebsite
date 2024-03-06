using Core.Application.Interfaces;
using Core.Application.ViewModels.Common;
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

        public async Task<IActionResult> Detail()
        {

			return View();
		}

		public async Task<IActionResult> Create()
		{

			return View();
		}

		public async Task<IActionResult> Update()
		{

			return View();
		}

        public async Task<IActionResult> Delete()
		{

			return View();
		}
	}
}
