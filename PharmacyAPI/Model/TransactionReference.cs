using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Model
{
    public class TransactionReference
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int CustomerTransactionId {get; set;}
        public CustomerTransaction CustomerTransaction {get; set;}

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public int Quantity {get; set;}

        [Required]
        public float Price{get; set;}
    }
}