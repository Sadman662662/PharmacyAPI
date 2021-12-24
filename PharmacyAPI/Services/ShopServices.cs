using PharmacyAPI.Contracts.V1.Request;
using PharmacyAPI.Data;
using PharmacyAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Services
{
    public class ShopServices : IShopService
    {
        private readonly PharmacyDBContext _pharmacydbcontext;
        public ShopServices(PharmacyDBContext pharmacyDBContext)
        {
            _pharmacydbcontext = pharmacyDBContext;
        }
        public async Task<bool> CreateShopAsync(Shop shop)
        {
            await _pharmacydbcontext.AddAsync(shop);
            var created = await _pharmacydbcontext.SaveChangesAsync();

            return created > 0;
        }

        public async Task<Shop> GetShopByIdAsync(int shopId)
        {
            return await _pharmacydbcontext.Shops.FindAsync(shopId);
        }

        public async Task<List<Shop>> GetShopsAsync()
        {
            return await _pharmacydbcontext.Shops.ToListAsync();
        }

        public async Task<bool> UpdateShopAsync(Shop shop)
        {
            _pharmacydbcontext.Shops.Update(shop);
            var updated = await _pharmacydbcontext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> UserOwnsShopAsync(int shopId, string userId)
        {
            var shop = await _pharmacydbcontext.Shops.AsNoTracking().SingleOrDefaultAsync(x => x.Id == shopId);

            if (shop == null || !shop.UserId.Equals(userId))
            {
                return false;
            }
            return true; 
        }
    }
}
