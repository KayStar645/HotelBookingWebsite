namespace Core.Application.ViewModels.Common
{
    public class BaseListRQ
    {
        public string? Filters { get; set; }


		public string? Search { get; set; }
		public string? Sorts { get; set; }
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
