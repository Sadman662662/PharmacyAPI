using PharmacyAPI.Contracts.V1;
using PharmacyAPI.Contracts.V1.Request;
using PharmacyAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using PharmacyAPI.Model;
using PharmacyAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PharmacyAPI.Contracts.V1.Response;
using PharmacyAPI.Extensions;

namespace PharmacyAPI.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerController : ControllerBase
    {
        public readonly PharmacyDBContext _pharmacyDBContext;
        public readonly IProductService _iproductServices;
        public readonly ICustomerService _customerService;

        public CustomerController(PharmacyDBContext pharmacyDBContext, IProductService productServices, ICustomerService customerService)
        {
            _pharmacyDBContext = pharmacyDBContext;
            _iproductServices = productServices;
            _customerService = customerService;
        }

        [HttpPost(ApiRoutes.Customers.Add)]
        public async Task<IActionResult> CreateCustomerTransaction([FromBody] CreateCustomerTransactionRequest request)
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
            var registeredcustomer = false;
            if (request.CustomerId > 0)
            {
                registeredcustomer = await _customerService.GetCustomerById(request.CustomerId.Value);
            }
            request.CreateTransaction(registeredcustomer);
            var productsExists = true;
            if (request.ProductLists.Count > 0)
            {
                foreach (var product in request.ProductLists)
                {
                    var productExistscheck = await _iproductServices.ProductByShopCount(request.ShopId, product.ProductId);
                    if (!productExistscheck)
                    {
                        productsExists = false;
                        break;
                    }
                    else
                    {
                        var doneTransaction = await _customerService.CreateCustomerTransaction(request);
                        if (doneTransaction)

                            return Ok();

                        else
                            return BadRequest("transaction not done properly");
                    }
                    
                }
            }
            return BadRequest("No product added to the cart..");

        }

        [HttpPost(ApiRoutes.Customers.Create)]
        public async Task<IActionResult> RegisterCustomers(CreateCustomersRequest request)
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
           
            {
                var doneCustomerRegistration = await _customerService.CreateCustomers(request);
                if (doneCustomerRegistration)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("registration was not successful..");
                }
            }
        }
    }
}


