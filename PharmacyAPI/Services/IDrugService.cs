using PharmacyAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Services
{
    public interface IDrugService
    {
        Task<List<DrugBrand>> GetDrugBrandsAsync();
        Task<List<DrugCompany>> GetDrugCompaniesAsync();
        Task<List<DrugGeneric>> GetDrugGenericsAsync();
    }
}
