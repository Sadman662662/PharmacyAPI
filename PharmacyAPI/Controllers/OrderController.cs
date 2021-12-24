using AutoMapper;
using PharmacyAPI.Contracts.V1;
using PharmacyAPI.Contracts.V1.Request;
using PharmacyAPI.Contracts.V1.Response;
using PharmacyAPI.Data;
using PharmacyAPI.Extensions;
using PharmacyAPI.Model;
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
    public class OrderController : ControllerBase
    {
        public readonly PharmacyDBContext _pharmacyDBContext;
        public readonly IProductService _iproductServices;
        public readonly IOrderService _iorderService;

        public OrderController(PharmacyDBContext pharmacyDBContext, IOrderService orderService, IProductService productService)
        {
            _pharmacyDBContext = pharmacyDBContext;
            _iorderService = orderService;
            _iproductServices = productService;
        }

        public IProductService ProductService { get; }

        [HttpPost(ApiRoutes.Orders.Add)]

        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            if (!ModelState.IsValid)
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
            var companyexists = await _iorderService.CompanyExists(request.CompanyId);
            if (companyexists)
            {
                var productsExists = true;
                if (request.productLists.Count > 0)
                {
                    foreach (var product in request.productLists)
                    {
                        var productExistscheck = await _iproductServices.ProductByShopCount(request.ShopId, product.ProductId);
                        if (!productExistscheck)
                        {
                            productsExists = false;
                            break;
                        }
                        else
                        {
                            var doneTransaction = await _iorderService.CreateOrderTransaction(request);
                            if (doneTransaction)

                                return Ok();

                            else
                                return BadRequest("transaction not done properly");
                        }

                    }

                }
                return BadRequest("Product does not exists..");
            }
            return BadRequest("Company does not exists..");
        }
    }
}
