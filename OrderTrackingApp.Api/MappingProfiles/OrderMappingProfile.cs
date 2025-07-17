using AutoMapper;
using OrderTrackingApp.Api.Models;
using OrderTrackingApp.Application.Commands.Orders;

namespace OrderTrackingApp.Api.MappingProfiles
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<CreateOrderRequest, CreateOrderCommand>();
            CreateMap<CreateOrderItemRequest, CreateOrderItemDto>();
        }
    }
}
