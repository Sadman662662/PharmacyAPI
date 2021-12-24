using System;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAPI.Contracts.V1.Request
{
    public class UserRegistrationRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(01)([0-9]{9})|([0-9]{7})$", ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Invalid Password Length")]
        public string Password { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Invalid Drug Licence No.")]
        public string DrugLicence { get; set; }
        public string Address { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Invalid FirstName Length")]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [RegularExpression("male|female|Male|Female", ErrorMessage = "The Gender must be either Male or Female only.")]
        public string Gender { get; set; }
    }
}
