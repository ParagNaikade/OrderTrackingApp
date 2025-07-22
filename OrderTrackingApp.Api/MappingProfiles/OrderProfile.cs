using AutoMapper;
using OrderTrackingApp.Api.Models;
using OrderTrackingApp.Application.Orders.Commands;

namespace OrderTrackingApp.Api.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderRequest, CreateOrderCommand>();
            CreateMap<CreateOrderItemRequest, CreateOrderItemDto>();
        }
    }
}
