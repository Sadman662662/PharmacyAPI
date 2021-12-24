using System.ComponentModel.DataAnnotations;

namespace PharmacyAPI.Controllers
{
    public class UpdateShopRequest
    {
        [Required(ErrorMessage = "ShopId Required")]
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 3, ErrorMessage = "Invalid Drug Licence")]
        public string DrugLicence { get; set; }

        [StringLength(255, MinimumLength = 3, ErrorMessage = "Invalid Name Length")]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Invalid Address Length")]
        public string Address { get; set; }
        public string City { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}