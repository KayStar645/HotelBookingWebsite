using Core.Application.ViewModels.GoogleDrives;

namespace Core.Application.Interfaces.GoogleDrive
{
	public interface IGoogleDriveService
	{
		Task<UploadVM> UploadFilesToGoogleDrive(UploadRQ pRequest);

		Task<UploadVM> GetFileInfoFromGoogleDrive(string filePath);
	}
}
