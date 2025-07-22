using MediatR;
using OrderTrackingApp.Application.Contracts;
using OrderTrackingApp.Application.Contracts.Orders;

namespace OrderTrackingApp.Application.Orders.Queries
{
    public class GetOrdersQueryHandler(IOrderReadRepository orderReadRepository) : IRequestHandler<GetOrdersQuery, PaginatedResult<OrderDto>>
    {
        public async Task<PaginatedResult<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            return await orderReadRepository.GetPaginatedOrdersAsync(
                        page: request.Page,
                        pageSize: request.PageSize,
                        status: request.Status,
                        fromDate: request.FromDate,
                        toDate: request.ToDate,
                        cancellationToken: cancellationToken);
        }
    }
}
