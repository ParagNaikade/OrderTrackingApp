using AutoMapper;
using OrderTrackingApp.Application.Contracts.Orders;
using OrderTrackingApp.ReadPersistence.Models;

namespace OrderTrackingApp.ReadPersistence.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderReadModel, OrderDto>().ReverseMap();
            CreateMap<OrderItemReadModel, OrderItemDto>().ReverseMap();
        }
    }
}
