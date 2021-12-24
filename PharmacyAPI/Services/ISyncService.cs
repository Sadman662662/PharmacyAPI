
using PharmacyAPI.Contracts.V1.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Services
{
    public interface ISyncService
    {
        Task<bool> SyncNow(CreateSyncRequest request);
    }
}
