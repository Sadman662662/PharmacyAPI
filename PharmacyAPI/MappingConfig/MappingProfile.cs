using AutoMapper;
using PharmacyAPI.Contracts.V1.Response;
using PharmacyAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.MappingConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Shop, ShopResponse>();
            CreateMap<Product, ProductResponse>();
            CreateMap<DrugBrand, DrugBrandResponse>();
        }
    }
}
