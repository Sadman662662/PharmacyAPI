using PharmacyAPI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Contracts.V1.Request
{
    public class CreateCustomerTransactionRequest
    {
        public int? CustomerId { get; set; }
        [Required]
        public int ShopId { get; set; }
        public string CustomerName { get; set; }
        [Required]
        public List<ProductList> ProductLists { get; set; }
        public void CreateTransaction(bool registeredCustomer)
        {
            if (registeredCustomer)
            {
                CustomerName = CustomerName;
                CustomerId = CustomerId;
            }
            else
            {
                CustomerName = "Customer";
                CustomerId = null;
            }
        }
    }
}
