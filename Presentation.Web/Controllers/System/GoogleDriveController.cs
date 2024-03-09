using Core.Application.Interfaces.GoogleDrive;
using Core.Application.ViewModels.GoogleDrives;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Web.Controllers.System
{
	public class GoogleDriveController : Controller
	{
		private readonly IGoogleDriveService _googleDriveService;

		public GoogleDriveController(IGoogleDriveService googleDriveService)
		{
			_googleDriveService = googleDriveService;
		}

		[HttpPost]
		public async Task<IActionResult> UploadFile([FromForm] UploadRQ pRequest)
		{
			try
			{
				var result = await _googleDriveService.UploadFilesToGoogleDrive(pRequest);
				return Ok(new { success = true, message = "Tải lên tệp thành công.", data = result });
			}
			catch
			{
				return BadRequest(new { success = false, message = "Lỗi khi tải lên tệp." });
			}
		}



		[HttpGet]
		public async Task<IActionResult> GetFileDrive([FromQuery] string pFilePath)
		{
			try
			{
				var result = await _googleDriveService.GetFileInfoFromGoogleDrive(pFilePath);
				return Ok(new { success = true, message = "Tải xuống tệp thành công." });
			}
			catch
			{
				return BadRequest(new { success = false, message = "Lỗi khi tải tệp xuống." });
			}
		}
	}
}
