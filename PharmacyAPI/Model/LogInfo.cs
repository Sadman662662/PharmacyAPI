using PharmacyAPI.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Model
{
    public class LogInfo
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [Required]
        [ForeignKey("DataCreatedBy")]
        public string CreatedBy { get; set; }
        public ApplicationUser DataCreatedBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ModifiedDate { get; set; }

        [ForeignKey("DataModifiedBy")]
        public string ModifiedBy { get; set; }
        public ApplicationUser DataModifiedBy { get; set; }
    }
}
