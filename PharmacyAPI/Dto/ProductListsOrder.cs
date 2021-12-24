using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Dto
{
    public class ProductListsOrder
    {
        public int ProductId { get; set; }
        public int DrugBrandId { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }


    }
}
