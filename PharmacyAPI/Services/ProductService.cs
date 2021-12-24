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
    public class ProductService : IProductService
    {
        private readonly PharmacyDBContext _pharmacyDbContext;
        private readonly IShopService _iShopService;
        private readonly IIdentityService _identityService;
        public ProductService(PharmacyDBContext pharmacyDbContext, IShopService iShopService, IIdentityService identityService)
        {
            _pharmacyDbContext = pharmacyDbContext;
            _iShopService = iShopService;
            _identityService = identityService;
        }
        public async Task<Product> GetProductByShopId(int productId,int shopId)
        {
            var product = await _pharmacyDbContext.Products.FindAsync(productId);
            if (!product.ShopId.Equals(shopId))
                return null;
            return product;
        }
        public async Task<IList<Product>> GetProductsByShopAsync(int shopId)
        {
            return await _pharmacyDbContext.Products.Include(x=> x.DrugBrand)
                .Where(x=> x.ShopId == shopId).ToListAsync();
        }
        public async Task<bool> CreateProductAsync(Product product)
        {
            using (_pharmacyDbContext)
            {
                using var transaction = _pharmacyDbContext.Database.BeginTransaction();
                try
                {
                    await _pharmacyDbContext.Products.AddAsync(product);
                    await _pharmacyDbContext.SaveChangesAsync();
                    var stockTransaction = new StockTransaction
                    {
                        ProductId = product.Id,
                        TransactionType = "Stock In",
                        TransactionDate = product.CreatedDate
                    };
                    await _pharmacyDbContext.StockTransactions.AddAsync(stockTransaction);
                    await _pharmacyDbContext.SaveChangesAsync();
                    transaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public async Task<bool> UpdateProductAsync(Product product)
        {
            using (_pharmacyDbContext)
            {
                using var transaction = _pharmacyDbContext.Database.BeginTransaction();
                try
                {
                    _pharmacyDbContext.Products.Attach(product);
                    _pharmacyDbContext.Entry(product).State = EntityState.Modified;
                    var stockTransaction = new StockTransaction
                    {
                        ProductId = product.Id,
                        TransactionType = "Product Updated",
                        TransactionDate = product.CreatedDate
                    };
                    await _pharmacyDbContext.StockTransactions.AddAsync(stockTransaction);
                    await _pharmacyDbContext.SaveChangesAsync();
                    transaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> ShopExistsAsync(int shopId)
        {
            var shop = await _pharmacyDbContext.Shops.FindAsync(shopId);
            if (shop != null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> UserOwnsShopAsync(int shopId, string email)
        {
            var shop = await _pharmacyDbContext.Shops.AsNoTracking().SingleOrDefaultAsync(x => x.Id == shopId);

            if (shop == null)
            {
                return false;
            }
            if (!shop.Email.Equals(email))
            {
                return false;
            }
            return true;
        }

        public async Task<(bool isOwner, string message)> CheckShopOwnerAsync(int shopId, string username)
        {
            var checkUser = await _identityService.UserExists(username);
            if (!checkUser)
            {
                return (false, "User not found !!");
            }
            var checkShop = await ShopExistsAsync(shopId);
            if (checkShop)
            {
                var loggedInUserId = await _identityService.GetUserId(username);
                var isShopOwner = await _iShopService.UserOwnsShopAsync(shopId, loggedInUserId.ToString());
                if (isShopOwner)
                    return (true, null);
                else
                    return (false, "User Does not own this shop");
            }
            else
                return (false, "Shop doesn't exists..");
        }

        public async Task<bool> GetCustomerByName(string username)
        {
            var customer = await _pharmacyDbContext.RegisteredCustomers.FindAsync(username);
            if (customer.Equals(true))
            {
                return true;
            }
            else
                return false;
        }

        public async Task<bool> ProductByShopCount(int shopId, int productId)
        {
            var product = await _pharmacyDbContext.Products
                .Where(p => p.Id == productId && p.ShopId == shopId).CountAsync();
            if (product > 0)
                return true;
            return false;
        }

    }
}
