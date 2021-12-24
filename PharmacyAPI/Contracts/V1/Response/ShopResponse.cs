using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Contracts.V1.Response
{
    public class ShopResponse
    {
        public int Id{ get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string DrugLicese { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
