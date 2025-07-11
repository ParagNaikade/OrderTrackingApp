using OrderTrackingApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTrackingApp.ReadPersistence.Interfaces
{
    public interface IOrderReadRepository
    {
        Task<OrderDto?> GetByIdAsync(Guid id);

        Task<List<OrderDto>> GetAllAsync();
    }
}
