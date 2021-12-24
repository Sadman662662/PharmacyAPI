using PharmacyAPI.Data;
using PharmacyAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Services
{
    public class OrderTransactionService : IOrderTransactionService
    {
        private readonly PharmacyDBContext _pharmacyDBContext;

        public OrderTransactionService(PharmacyDBContext pharmacyDBContext)
        {
            _pharmacyDBContext = pharmacyDBContext;
        }

        public async Task<bool> CreateorderReferenceAsync(orderReference orderReference)
        {
            await _pharmacyDBContext.OrderReferences.AddAsync(orderReference);
            var created = await _pharmacyDBContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> CreateOrderTransactionAsync(Order order)
        {
            await _pharmacyDBContext.Orders.AddAsync(order);
            var created = await _pharmacyDBContext.SaveChangesAsync();
            return created > 0;
        }
    }
}
