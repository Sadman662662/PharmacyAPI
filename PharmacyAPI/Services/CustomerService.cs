using PharmacyAPI.Contracts.V1.Request;
using PharmacyAPI.Data;
using PharmacyAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly PharmacyDBContext _pharmacydbcontext;
        private readonly ICustomerTransactionService _customerTransactionService;
        public CustomerService(PharmacyDBContext pharmacyDBContext, ICustomerTransactionService customerTransactionService)
        {
            _pharmacydbcontext = pharmacyDBContext;
            _customerTransactionService = customerTransactionService;
        }
        public async Task<bool> CreateCustomerTransaction(CreateCustomerTransactionRequest request)
        {
            using (_pharmacydbcontext)
            {
                using var transaction = _pharmacydbcontext.Database.BeginTransaction();
                try
                {
                        var customerTarnsaction = new CustomerTransaction
                        {
                            ShopId = request.ShopId,
                            CustomerName = request.CustomerName,
                            CustomerId = request.CustomerId,
                            TransactionType = "Sold",
                            TransactionDate = DateTime.UtcNow
                        };
                        
                        await _customerTransactionService.CreateCustomerTransactionAsync(customerTarnsaction);
                        foreach (var item in request.ProductLists)
                        {
                            var transactionReference = new TransactionReference
                            {
                                CustomerTransactionId = customerTarnsaction.Id,
                                ProductId = item.ProductId,
                                Quantity = item.Quantity,
                                Price = 5
                            };
                            await _customerTransactionService.CreateTransactionReferenceAsync(transactionReference);
                            var stockTransaction = new StockTransaction
                            {
                                ProductId = transactionReference.ProductId,
                                TransactionType = "Sold",
                                TransactionDate = customerTarnsaction.TransactionDate
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

        public async Task<bool> GetCustomerById(int id)
        {
            return await _pharmacydbcontext.Customers.Where(c => c.Id == id).CountAsync() > 0;
        }

        public async Task<bool> CustomerExists(string mobileNumber)
        {
            var customer = await _pharmacydbcontext.Customers.FindAsync(mobileNumber);
            if (customer != null)
            {
                return false;
            }
            else
                return true;
        }

        public async Task<bool> CreateCustomers(CreateCustomersRequest request)
        {
                try
                {
                    var customer = new Customer
                    {
                        ShopId = request.ShopId,
                        Name = request.Name,
                        MobileNumber = request.MobileNumber,
                        Due = request.Due
                    };
                await CreateCustomersAsync(customer);
                    return true;
                }
                
                catch (Exception)
                {
                    return false;
                }
            
        }

        public async Task<bool> CreateCustomersAsync(Customer customer)
        {
            await _pharmacydbcontext.Customers.AddAsync(customer);
            var created = await _pharmacydbcontext.SaveChangesAsync();
            return created > 0;
        }

        
    }
}
