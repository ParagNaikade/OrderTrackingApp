namespace OrderTrackingApp.Application.Contracts
{
    public class PaginatedResult<T>(List<T> items, long totalCount, int page, int pageSize)
    {
        public List<T> Items { get; } = items;

        public long TotalCount { get; } = totalCount;

        public int Page { get; } = page;

        public int PageSize { get; } = pageSize;
    }
}
