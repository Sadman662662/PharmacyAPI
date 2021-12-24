using PharmacyAPI.Contracts.V1.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Services
{
    public interface ICustomerService
    {
        Task<bool> GetCustomerById(int id);
        Task<bool> CreateCustomerTransaction(CreateCustomerTransactionRequest transaction);
        Task<bool> CustomerExists(string mobileNumber);
        Task<bool> CreateCustomers(CreateCustomersRequest request);
    }
}
