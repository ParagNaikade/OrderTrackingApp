using AutoMapper;
using OrderTrackingApp.Application.Contracts.Orders;
using OrderTrackingApp.Domain.Entities;

namespace OrderTrackingApp.Application.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        }
    }
}
