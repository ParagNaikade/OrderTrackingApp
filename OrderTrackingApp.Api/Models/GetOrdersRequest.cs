namespace OrderTrackingApp.Api.Models
{
    public class GetOrdersRequest
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 50;

        public string? Status { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
