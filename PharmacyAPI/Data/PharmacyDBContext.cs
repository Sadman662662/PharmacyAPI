using PharmacyAPI.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PharmacyAPI.Data
{
    public class PharmacyDBContext : IdentityDbContext<ApplicationUser>
    {
        public PharmacyDBContext(DbContextOptions<PharmacyDBContext> options) :
            base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<DrugCompany> DrugCompanyList { get; set; }
        public DbSet<DrugBrand> DrugBrands { get; set; }
        public DbSet<DrugGeneric> DrugGenerics { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CustomerTransaction> CustomerTransactions { get; set; }
        public DbSet<Customer> RegisteredCustomers { get; set; }
        public DbSet<TransactionReference> TransactionReference {get; set;}
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<orderReference> OrderReferences { get; set; }
    }
}





