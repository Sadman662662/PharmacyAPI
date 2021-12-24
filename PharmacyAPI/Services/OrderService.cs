using PharmacyAPI.Contracts.V1.Request;
using PharmacyAPI.Data;
using PharmacyAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly PharmacyDBContext _pharmacydbcontext;
        private readonly IOrderTransactionService _orderTransactionService;
        private readonly ICustomerTransactionService _customerTransactionService;
        public OrderService(PharmacyDBContext pharmacyDBContext, IOrderTransactionService orderTransactionService, ICustomerTransactionService customerTransactionService)
        {
            _pharmacydbcontext = pharmacyDBContext;
            _orderTransactionService = orderTransactionService;
            _customerTransactionService = customerTransactionService;
        }

        public async Task<bool> CreateOrderTransaction(CreateOrderRequest request)
        {
            using (_pharmacydbcontext)
            {
                using var transaction = _pharmacydbcontext.Database.BeginTransaction();
                try
                {
                    var order = new Order
                    {
                        ShopId = request.ShopId,
                        DrugCompanyId = request.CompanyId,
                        TransactionType = "stock in",
                        TransctionDate = DateTime.UtcNow
                    };
                    
                    await _orderTransactionService.CreateOrderTransactionAsync(order);
                    foreach (var item in request.productLists)
                    {
                        var orderReference = new orderReference
                        {
                            OrderId = order.Id,
                            DrugBrandId = item.DrugBrandId,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            Price = item.Price
                        };
                        await _orderTransactionService.CreateorderReferenceAsync(orderReference);
                        var stockTransaction = new StockTransaction
                        {
                            ProductId = orderReference.ProductId,
                            TransactionType = "Sold",
                            TransactionDate = order.TransctionDate
                        };
                        await _customerTransactionService.CreateStockTransactionAsync(stockTransaction);
                    }
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> CompanyExists(int companyId)
        {
            var company = await _pharmacydbcontext.DrugCompanyList.FindAsync(companyId);
            if (company != null)

                return true;

            else

                return false;
        }
    }
}


