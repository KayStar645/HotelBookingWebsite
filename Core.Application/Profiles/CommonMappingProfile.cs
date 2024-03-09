using AutoMapper;
using Core.Application.Interfaces.GoogleDrive;
using Core.Application.ViewModels.GoogleDrives;

namespace Core.Application.Profiles
{
	public class CommonMappingProfile : Profile
	{
		protected static IGoogleDriveService? _googleDriveService;

		public CommonMappingProfile(IGoogleDriveService googleDriveService)
		{
			_googleDriveService = googleDriveService ?? throw new ArgumentNullException(nameof(googleDriveService));

			CreateMap<string, List<string>>().ConvertUsing<StringToListTypeConverter>();
			CreateMap<List<string>, string>().ConvertUsing<ListToStringTypeConverter>();

			CreateMap<string, UploadVM>().ConvertUsing<StringToFileDtoConverter>();
			CreateMap<UploadVM, string>().ConvertUsing<FileDtoToStringConvert>();

			CreateMap<string, List<UploadVM>>().ConvertUsing<StringToFileDtoListConverter>();
			CreateMap<List<UploadVM>, string>().ConvertUsing<FileDtoListToStringConvert>();
		}

		#region Mapping string và list<string>
		public class StringToListTypeConverter : ITypeConverter<string, List<string>>
		{
			public List<string> Convert(string source, List<string> destination, ResolutionContext context)
			{
				if (string.IsNullOrEmpty(source))
				{
					return new List<string>();
				}

				return source.Split(',').ToList();
			}
		}

		public class ListToStringTypeConverter : ITypeConverter<List<string>, string>
		{
			public string Convert(List<string> source, string destination, ResolutionContext context)
			{
				if (source == null || source.Count == 0)
				{
					return null;
				}

				return string.Join(",", source);
			}
		}
		#endregion

		public class StringToFileDtoConverter : ITypeConverter<string, UploadVM>
		{
			public UploadVM Convert(string source, UploadVM destination, ResolutionContext context)
			{
				if (string.IsNullOrEmpty(source))
				{
					return new UploadVM();
				}

				var result = _googleDriveService.GetFileInfoFromGoogleDrive(source).Result;
				return result ?? new UploadVM();
			}
		}

		public class FileDtoToStringConvert : ITypeConverter<UploadVM, string>
		{
			public string Convert(UploadVM source, string destination, ResolutionContext context)
			{
				return source?.Path ?? string.Empty;
			}
		}

		public class StringToFileDtoListConverter : ITypeConverter<string, List<UploadVM>>
		{
			public List<UploadVM> Convert(string source, List<UploadVM> destination, ResolutionContext context)
			{
				if (string.IsNullOrEmpty(source))
				{
					return new List<UploadVM>();
				}

				var imagePaths = source.Split(',');
				var fileDtos = new List<UploadVM>();

				foreach (var imagePath in imagePaths)
				{
					var result = _googleDriveService.GetFileInfoFromGoogleDrive(imagePath).Result;
					if (result != null)
					{
						fileDtos.Add(result);
					}
				}

				return fileDtos;
			}
		}

		public class FileDtoListToStringConvert : ITypeConverter<List<UploadVM>, string>
		{
			public string Convert(List<UploadVM> source, string destination, ResolutionContext context)
			{
				if (source == null || source.Count == 0)
				{
					return null;
				}

				var paths = source.Select(fileDto => fileDto.Path);
				return string.Join(",", paths);
			}
		}
	}
}
