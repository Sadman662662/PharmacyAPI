using PharmacyAPI.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Contracts.V1.Request
{
    public class CreateOrderRequest
    {
        [Required]
        public int ShopId { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public List<ProductListsOrder> productLists { get; set; }

       
    }
}
