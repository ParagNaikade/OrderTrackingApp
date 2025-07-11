using OrderTrackingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTrackingApp.Application.Interfaces
{
    public interface IOrderWriteRepository
    {
        Task AddAsync(Order order);

        Task UpdateAsync(Order order);
        
        Task DeleteAsync(Guid id);
    }
}
