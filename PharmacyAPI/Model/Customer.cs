using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Model
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ShopId { get; set; }
        public Shop Shop { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        [Required]
        public int Due { get; set; }


    }
}
