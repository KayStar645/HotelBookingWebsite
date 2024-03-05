namespace Core.Application.Responses
{
    public class PaginatedResult<T>
    {
        public Extra? Extra { get; set; }

        public T? Data { get; set; }

        public PaginatedResult(T? pData = default, int pCount = 0, int? pCurrentPage = 1, int? pPageSize = 30)
        {
            Data = pData;
            Extra = new Extra(pCount, pCurrentPage, pPageSize);
        }

        public static PaginatedResult<T> Create(T pData, int pCount, int pPageNumber, int pPageSize, int pCode)
        {
            return new PaginatedResult<T>(pData, pCount, pPageNumber, pPageSize);
        }

    }
}
