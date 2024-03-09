using Microsoft.AspNetCore.Http;

namespace Core.Application.ViewModels.GoogleDrives
{
	public class UploadRQ
	{
		public IFormFile? File { get; set; }

		public string? FileName { get; set; }
	}
}
