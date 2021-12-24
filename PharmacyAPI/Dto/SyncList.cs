using PharmacyAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Dto
{
    public class SyncList
    {
        public List<CustomerTransaction> CustomerTransactions { get; set; }
        public List<TransactionReference> TransactionReferences { get; set; }
    }
}
