using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Model
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ShopId { get; set; }
        public Shop Shop { get; set; }

        [Required]
        public int DrugCompanyId { get; set; }
        public DrugCompany DrugCompany { get; set; }

        [Required]
        public string TransactionType { get; set; }

        [Required]
        public DateTime TransctionDate { get; set; }
    }
}

