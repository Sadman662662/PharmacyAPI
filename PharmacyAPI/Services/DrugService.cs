using PharmacyAPI.Data;
using PharmacyAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Services
{
    public class DrugService : IDrugService
    {
        private readonly PharmacyDBContext _pharmacydbcontext;
        public DrugService(PharmacyDBContext pharmacyDBContext)
        {
            _pharmacydbcontext = pharmacyDBContext;
        }

        public async Task<List<DrugBrand>> GetDrugBrandsAsync()
        {
            return await _pharmacydbcontext.DrugBrands.ToListAsync();
        }
        public async Task<List<DrugCompany>> GetDrugCompaniesAsync()
        {
            return await _pharmacydbcontext.DrugCompanyList.ToListAsync();
        }
        public async Task<List<DrugGeneric>> GetDrugGenericsAsync()
        {
            return await _pharmacydbcontext.DrugGenerics.ToListAsync();
        }
    }
}
