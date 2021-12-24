using PharmacyAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Services
{
    public interface ICustomerTransactionService
    {
        Task<bool> CreateCustomerTransactionAsync(CustomerTransaction customerTransaction);
        Task<bool> CreateTransactionReferenceAsync(TransactionReference transactionReference);
        Task<bool> CreateStockTransactionAsync(StockTransaction stockTransaction);
    }
}
