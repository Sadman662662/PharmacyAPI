using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int DrugBrandId { get; set; }
        public DrugBrand DrugBrand { get; set; }
        [Required]
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        [Required]
        public float UnitPrice { get; set; }
        public int? UnitInStock { get; set; }
        [Required]
        public bool IsBelowMinimalStock { get; set; }
        [Required]
        public bool Discontinued { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

