using PharmacyAPI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Contracts.V1.Request
{
    public class CreateProductRequest
    {
        [Required]
        public int DrugBrandId { get; set; }
        [Required]
        public int ShopId { get; set; }
        public int? UnitInStock { get; set; }
        [Required]
        public float UnitPrice { get; set; }
        [Required]
        public bool IsBelowMinimalStock { get; set; }
        [Required]
        public bool Discontinued { get; set; }
    }

    public class UpdateProductRequest 
    {
        [Required]
        public int Id { get; set; }
        public int? DrugBrandId { get; set; }
        [Required]
        public int ShopId { get; set; }
        public int? UnitInStock { get; set; }
        public float? UnitPrice { get; set; }
        [Required]
        public bool IsBelowMinimalStock { get; set; }
        [Required]
        public bool Discontinued { get; set; }
    }
}
