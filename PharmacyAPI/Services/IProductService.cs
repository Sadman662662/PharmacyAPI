using PharmacyAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Services
{
    public interface IProductService
    {
        Task<bool> ShopExistsAsync(int shopId);
        Task<bool> CreateProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<(bool isOwner, string message)> CheckShopOwnerAsync(int shopId, string username);
        Task<Product> GetProductByShopId(int productId, int shopId);
        Task<IList<Product>> GetProductsByShopAsync(int shopId);
        Task<bool> ProductByShopCount(int productId, int shopId);
        Task<bool> GetCustomerByName(string username);
    }
}
