using System.ComponentModel.DataAnnotations;

namespace PharmacyAPI.Services
{
    public class CreateCustomersRequest
    {
        [Required]
        public int ShopId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        [Required]
        public int Due { get; set; }
    }
}