using MediatR;
using OrderTrackingApp.Application.Contracts;
using OrderTrackingApp.Application.Contracts.Orders;

namespace OrderTrackingApp.Application.Orders.Queries
{
    public record GetOrdersQuery : IRequest<PaginatedResult<OrderDto>>
    {
        public int Page { get; set; } = 1;
        
        public int PageSize { get; set; } = 50;
        
        public string? Status { get; set; }
        
        public DateTime? FromDate { get; set; }
        
        public DateTime? ToDate { get; set; }
    }
}
