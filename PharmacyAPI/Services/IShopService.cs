using PharmacyAPI.Contracts.V1.Request;
using PharmacyAPI.Data;
using PharmacyAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Services
{
    public interface IShopService
    {
        Task<List<Shop>> GetShopsAsync();
        Task<Shop> GetShopByIdAsync(int shopId);
        Task<bool> CreateShopAsync(Shop shop);
        Task<bool> UpdateShopAsync(Shop shop);
        Task<bool> UserOwnsShopAsync(int shopId, string userId);
    }
}
