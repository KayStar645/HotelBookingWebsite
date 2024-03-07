namespace Core.Application.Responses
{
    public class PaginatedResult<T>
    {
        public Extra? Extra { get; set; }

        public List<T> Data { get; set; }

        public PaginatedResult(List<T> pData, int pCount = 0, int? pCurrentPage = 1, int? pPageSize = 30)
        {
            Data = pData;
            Extra = new Extra(pCount, pCurrentPage, pPageSize);
        }

        public static PaginatedResult<T> Create(List<T> pData, int pCount, int pPageNumber, int pPageSize)
        {
            return new PaginatedResult<T>(pData, pCount, pPageNumber, pPageSize);
        }

    }
}
