namespace Core.Application.Responses
{
    public class Extra
    {
        public int? CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int? PageSize { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public Extra(int pCount, int? pCurrentPage, int? pPageSize)
        {
            CurrentPage = pCurrentPage;
            PageSize = pPageSize;
            TotalPages = (int)Math.Ceiling(pCount / (double)pPageSize);
            TotalCount = pCount;
        }

    }
}
