namespace OpsFlow.Application.Incidents.Dtos
{
    public class PaginatedResponseDto<T>
    {
        public IReadOnlyList<T> Items { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }
        public int TotalPages { get; init; }

        public PaginatedResponseDto
        (
            IReadOnlyList<T> items,
            int pageNumber,
            int pageSize,
            int totalCount
        )
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}