using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webWS
{
    public class SD //static detail
    {
        public static string APIBaseUrl = "https://localhost:44365/";
        public static string LocationApiPath = APIBaseUrl + "api/Locations/";
        public static string ProductsApiPath = APIBaseUrl + "api/Products/";
        public static string ProductsByCategoryApiPath = APIBaseUrl + "api/Products/category/";
        public static string CategoryMeniApiPath = APIBaseUrl + "api/Products/categoryMeni/";
        public static string AccountApiPath = APIBaseUrl + "api/Account/";
    }
}
