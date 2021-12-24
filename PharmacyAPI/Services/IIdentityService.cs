using PharmacyAPI.Contracts.V1.Request;
using PharmacyAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(UserRegistrationRequest user);
        Task<AuthenticationResult> LoginAsync(string username, string password);
        Task<bool> UserExists(string username);
        Task<Guid> GetUserId(string username);
    }
}
