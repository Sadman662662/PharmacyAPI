using PharmacyAPI.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Contracts.V1.Request
{
    public class CreateSyncRequest
    {
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int ShopId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public List<SyncList> SyncLists { get; set; }
    }
}

