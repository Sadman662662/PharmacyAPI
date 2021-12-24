using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;
        
        public static class Identity
        {
            public const string Login = Base + "/identity/login";

            public const string Register = Base + "/identity/register";
        }

        public static class Shops
        {
            public const string GetAll = Base + "/shops";

            public const string Get = Base + "/shops/{shopId}";

            public const string Create = Base + "/shops";

            public const string Update = Base + "/shops";
        }

        public static class Products
        {
            public const string Add = Base + "/products";
            public const string Update = Base + "/products";
            public const string Get = Base + "/products/{productId}";
            public const string GetShopProducts = Base + "/products/{shopId}";
        }
        public static class Drugs
        {
            public const string DrugBrands = Base + "/DrugBrands";
            public const string DrugGenericss = Base + "/DrugGenerics";
            public const string DrugCompanies = Base + "/DrugCompanies";
        }

        public static class Customers
        {
            public const string Add = Base + "/customers";
            public const string Get = Base + "/customers/{customerId}";
            public const string Create = Base + "customers";

        }

        public static class Orders
        {
            public const string Add = Base + "/orders";
            public const string Get = Base + "/orderId";
        }

        public static class Sycn
        {
            public const string Add = Base + "/customertransactions";
        }
    }
}
