using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PharmacyAPI.Contracts.V1;
using PharmacyAPI.Contracts.V1.Request;
using PharmacyAPI.Contracts.V1.Response;
using PharmacyAPI.Extensions;
using PharmacyAPI.Model;
using PharmacyAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PharmacyAPI.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public ShopController(IShopService shopService, IIdentityService identityService, IMapper mapper) 
        {
            _shopService = shopService;
            _identityService = identityService;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Shops.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var shopList = await _shopService.GetShopsAsync();
            var response = _mapper.Map<List<ShopResponse>>(shopList);
            return Ok(response);
        }

        [HttpGet(ApiRoutes.Shops.Get)]
        public async Task<IActionResult> Get([FromRoute] int shopId)
        {
            var shop = await _shopService.GetShopByIdAsync(shopId);
            if (shop == null)
                return NotFound();
            else
            {
                var response = _mapper.Map<ShopResponse>(shop);
                return Ok(response);
            }
        }

        [HttpPost(ApiRoutes.Shops.Create)]
        public async Task<IActionResult> Create([FromBody] CreateShopRequest shopRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Faildesponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }
            var loggedInUser = HttpContext.GetUserName();
            var checkUser = await _identityService.UserExists(loggedInUser);
            if (!checkUser)
                return BadRequest("User not found !!");
            var userId = HttpContext.GetUserId();

            var shop = shopRequest.CreateShop(userId);

            await _shopService.CreateShopAsync(shop);
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Shops.Get.Replace("{shopId}", shop.Id.ToString());

            var response = new { shop.Id };
            return Created(locationUri, response);
        }

        [HttpPut(ApiRoutes.Shops.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateShopRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Faildesponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            var shop = await _shopService.GetShopByIdAsync(request.Id);
            if (shop == null)
                return BadRequest("No Shop Found");

            var loggedInUser = HttpContext.GetUserName();
            var loggedInUserId = HttpContext.GetUserId();
            var userOwnsShop = await _shopService.UserOwnsShopAsync(request.Id, loggedInUserId);
            if (!userOwnsShop)
                return Unauthorized(new { error = "You are not allowed!!" });

            shop.Name = request.Name ?? shop.Name;
            shop.Email = request.Email ?? shop.Email;
            shop.Address = request.Address ?? shop.Address;
            shop.City = request.City ?? shop.City;
            shop.DrugLicence = request.DrugLicence ?? shop.DrugLicence;
            shop.IsActive = request.IsActive;
            shop.ModifiedBy = loggedInUserId;
            shop.ModifiedDate = DateTime.Now.Date;
            
            var updated = await _shopService.UpdateShopAsync(shop);
            if (updated)
                return Ok("Updated Successfully");
            return StatusCode(StatusCodes.Status500InternalServerError, "Operation Failed. Please Try agin !!");
        }
    }
}