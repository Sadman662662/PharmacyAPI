using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Contracts.V1.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public int DrugBrandId { get; set; }
        public DrugBrandResponse DrugBrand { get; set; }
        public float UnitPrice { get; set; }
        public int? UnitInStock { get; set; }
        public bool IsBelowMinimalStock { get; set; }
        public bool Discontinued { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class DrugBrandResponse
    {
        public string Name { get; set; }
        public string Form { get; set; }
        public string Strength { get; set; }
        public float Price { get; set; }
    }
}
