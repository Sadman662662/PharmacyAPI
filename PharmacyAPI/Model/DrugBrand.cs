using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Model
{
    public class DrugBrand
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int DrugGenericId { get; set; }
        public DrugGeneric DrugGeneric { get; set; }
        [Required]
        public int DrugCompanyId { get; set; }
        public DrugCompany DrugCompany { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Form { get; set; }
        public string Strength { get; set; }
        public float Price { get; set; }
    }
}



