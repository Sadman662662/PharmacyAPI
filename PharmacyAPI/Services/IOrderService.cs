using PharmacyAPI.Contracts.V1.Request;
using PharmacyAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Services
{
    public interface IOrderService
    {
        Task<bool> CompanyExists(int companyId);
        Task<bool> CreateOrderTransaction(CreateOrderRequest request);
    }
}
