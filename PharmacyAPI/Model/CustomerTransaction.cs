using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Model
{
    public class CustomerTransaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ShopId { get; set; }
        public Shop Shop { get; set; }

        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string TransactionType { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }


    }
}


