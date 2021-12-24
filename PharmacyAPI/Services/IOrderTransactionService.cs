using PharmacyAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Services
{
    public interface IOrderTransactionService 
    {
        Task<bool> CreateOrderTransactionAsync(Order order);
        Task<bool> CreateorderReferenceAsync(orderReference orderReference);
    }
}
