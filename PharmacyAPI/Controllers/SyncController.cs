using PharmacyAPI.Contracts.V1;
using PharmacyAPI.Contracts.V1.Request;
using PharmacyAPI.Contracts.V1.Response;
using PharmacyAPI.Data;
using PharmacyAPI.Extensions;
using PharmacyAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SyncController : ControllerBase
    {
        private readonly ISyncService _syncService;
        private readonly IProductService _iproductServices;
        

        public SyncController(ISyncService syncService, IProductService productService)
        {
            _syncService = syncService;
            _iproductServices = productService;
        }

        public IProductService ProductService { get; }

        [HttpPost(ApiRoutes.Sycn.Add)]
        public async Task<IActionResult> SyncTransactions(CreateSyncRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new Faildesponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }
            var loggedInUser = HttpContext.GetUserName();
            var (isOwner, message) = await _iproductServices.CheckShopOwnerAsync(request.ShopId, loggedInUser);
            if (!isOwner)
                return BadRequest(message);
            return Ok();
        }
    }
}
