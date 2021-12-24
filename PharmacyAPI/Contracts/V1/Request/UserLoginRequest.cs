using System.ComponentModel.DataAnnotations;

namespace PharmacyAPI.Contracts.V1.Request
{
    public class UserLoginRequest
    {
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
