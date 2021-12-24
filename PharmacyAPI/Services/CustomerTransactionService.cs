using PharmacyAPI.Data;
using PharmacyAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Services
{
    public class CustomerTransactionService : ICustomerTransactionService
    {
        private readonly PharmacyDBContext _pharmacydbcontext;

        public CustomerTransactionService(PharmacyDBContext pharmacyDBContext)
        {
            _pharmacydbcontext = pharmacyDBContext;
        }

        public async Task<bool> CreateCustomerTransactionAsync(CustomerTransaction customerTransaction)
        {
            await _pharmacydbcontext.CustomerTransactions.AddAsync(customerTransaction);
            try
            {
                var created = await _pharmacydbcontext.SaveChangesAsync();
                return created > 0;
            }
            catch (Exception e )
            {
                
                throw e;
                
            }
        }

        public async Task<bool> CreateStockTransactionAsync(StockTransaction stockTransaction)
        {
            await _pharmacydbcontext.StockTransactions.AddAsync(stockTransaction);
            var created = await _pharmacydbcontext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> CreateTransactionReferenceAsync(TransactionReference transactionReference)
        {
            await _pharmacydbcontext.TransactionReference.AddAsync(transactionReference);
            var created = await _pharmacydbcontext.SaveChangesAsync();
            return created > 0;
        }

       
    }
}
