using AutoMapper;

namespace Core.Application.Profiles
{
    public class CommonMappingProfile : Profile
    {
        public CommonMappingProfile()
        {
			CreateMap<string, List<string>>().ConvertUsing<StringToListTypeConverter>();
			CreateMap<List<string>, string>().ConvertUsing<ListToStringTypeConverter>();
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
	}
}
