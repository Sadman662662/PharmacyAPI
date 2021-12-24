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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _iProductService;
        private readonly IMapper _mapper;
        public ProductController(IProductService iProductService, IMapper mapper)
        {
            _iProductService = iProductService;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Products.GetShopProducts)]
        public async Task<IActionResult> GetProductsByShop([FromRoute] int shopId)
        {
            var loggedInUser = HttpContext.GetUserName();
            var (isOwner, message) = await _iProductService.CheckShopOwnerAsync(shopId, loggedInUser);
            if (!isOwner)
                return BadRequest(message);

            var product = await _iProductService.GetProductsByShopAsync(shopId);
            var response = _mapper.Map<List<ProductResponse>>(product);
            return Ok(response);
        }

        [HttpPost(ApiRoutes.Products.Add)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Faildesponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }
            var loggedInUser = HttpContext.GetUserName();
            var (isOwner, message) = await _iProductService.CheckShopOwnerAsync(request.ShopId, loggedInUser);
            if (!isOwner)
                return BadRequest(message);

            var product = new Product
            {
                DrugBrandId = request.DrugBrandId,
                ShopId = request.ShopId,
                UnitPrice = request.UnitPrice,
                UnitInStock = request.UnitInStock,
                Discontinued = request.Discontinued,
                IsBelowMinimalStock = request.IsBelowMinimalStock,
                CreatedDate = DateTime.Now,
                CreatedBy = loggedInUser
            };
            var result = _iProductService.CreateProductAsync(product).Result;
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/";
            var response = new { product.Id };

            if (result)
                return Created(locationUri, response);

            return StatusCode(StatusCodes.Status500InternalServerError, "Operation Failed. Please Try agin !!");
        }

        [HttpPut(ApiRoutes.Products.Update)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Faildesponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            var loggedInUser = HttpContext.GetUserName();
            var (isOwner, message) = await _iProductService.CheckShopOwnerAsync(request.ShopId, loggedInUser);
            if (!isOwner)
                return BadRequest(message);
            var product = await _iProductService.GetProductByShopId(request.Id, request.ShopId);
            if(product == null)
                return BadRequest("Product Not Found");

            product.DrugBrandId = request.DrugBrandId ?? product.DrugBrandId;
            product.UnitPrice = request.UnitPrice ?? product.UnitPrice;
            product.Discontinued = request.Discontinued;
            product.IsBelowMinimalStock = request.IsBelowMinimalStock;
            product.ModifiedBy = loggedInUser;
            product.ModifiedDate = DateTime.Now;

            var result = _iProductService.UpdateProductAsync(product).Result;
            if (result)
                return Ok("Product Updated Successfully");

            return StatusCode(StatusCodes.Status500InternalServerError, "Update Failed. Please Try agin !!");
        }
    }
}
