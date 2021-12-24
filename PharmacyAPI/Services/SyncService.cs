using PharmacyAPI.Contracts.V1.Request;
using PharmacyAPI.Data;
using PharmacyAPI.Dto;
using PharmacyAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Services
{
    public class SyncService : ISyncService
    {
        private readonly PharmacyDBContext _pharmacyDBContext;
        private readonly ICustomerTransactionService _customerTransactionService;

        public SyncService(PharmacyDBContext pharmacyDBContext, ICustomerTransactionService customerTransactionService)
        {
            _pharmacyDBContext = pharmacyDBContext;
            _customerTransactionService = customerTransactionService;
        }

        public Task<bool> SyncNow(CreateSyncRequest request)
        {
            throw new NotImplementedException();
        }

        //public async Task<bool> SyncNow(CreateSyncRequest request)
        //{
        //    foreach (var item in request.SyncLists)
        //    {
        //        using (_pharmacyDBContext)
        //        {
        //            using var transaction = _pharmacyDBContext.Database.BeginTransaction();
        //            try
        //            {
        //                var customerTarnsaction = new CustomerTransaction
        //                {
        //                    ShopId = request.ShopId,
        //                    CustomerName = request.CustomerName,
        //                    CustomerId = request.CustomerId,
        //                    TransactionType = "Sold",
        //                    TransactionDate = DateTime.UtcNow
        //                };

        //                await _customerTransactionService.CreateCustomerTransactionAsync(customerTarnsaction);
        //                foreach (var item1 in request.SyncLists)
        //                {
        //                    var transactionReference = new TransactionReference
        //                    {
        //                        CustomerTransactionId = customerTarnsaction.Id,
        //                        ProductId = item1.ProductId,
        //                        Quantity = item1.Quantity,
        //                        Price = 5
        //                    };
        //                    await _customerTransactionService.CreateTransactionReferenceAsync(transactionReference);
        //                }
        //                transaction.Commit();
        //                return true;
        //            }
        //            catch (Exception)
        //            {
        //                transaction.Rollback();
        //                return false;
        //            }
        //        }
        //    }
        //    return 
        //}
    }
}
