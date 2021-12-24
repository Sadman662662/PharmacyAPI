using PharmacyAPI.Data;
using PharmacyAPI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Contracts.V1.Request
{
    public class CreateShopRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Invalid Drug Licence")]
        public string DrugLicence { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Invalid Name Length")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Invalid Address Length")]
        public string Address { get; set; }

        public string City { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public Shop CreateShop(string userId)
        {
            return new Shop
            {
                UserId = userId,
                Email = Email,
                Name = Name,
                DrugLicence = DrugLicence,
                Address = Address,
                City = City,
                IsActive = IsActive,
                CreatedBy = userId,
                CreatedDate = DateTime.Now.Date
            };
        }
    }
}



