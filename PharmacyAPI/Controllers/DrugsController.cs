using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PharmacyAPI.Contracts.V1;
using PharmacyAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PharmacyAPI.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DrugsController : ControllerBase
    {
        private readonly IDrugService _drugService;
        public DrugsController(IDrugService drugService)
        {
            _drugService = drugService;
        }

        [HttpGet(ApiRoutes.Drugs.DrugBrands)]
        public async Task<IActionResult> GetDrugBrands()
        {
            return Ok(await _drugService.GetDrugBrandsAsync());
        }

        [HttpGet(ApiRoutes.Drugs.DrugCompanies)]
        public async Task<IActionResult> GetDrugCompanies()
        {
            return Ok(await _drugService.GetDrugCompaniesAsync());
        }

        [HttpGet(ApiRoutes.Drugs.DrugGenericss)]
        public async Task<IActionResult> GetDrugGenerics()
        {
            return Ok(await _drugService.GetDrugGenericsAsync());
        }
    }
}