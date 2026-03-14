namespace OpsFlow.Application.Incidents.Dtos
{
    public sealed record PaginatedResponseDto<T>
    (
        IReadOnlyList<T> Items,
        int PageNumber,
        int PageSize,
        int TotalCount,
        int TotalPages = (int)Math.Ceiling(TotalCount / (double)Pagesize)
    );
}