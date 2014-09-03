using System.Web;
using System.Web.Mvc;
using Hsr.Core.Filters;

namespace Hsr
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
           // filters.Add(new AuthorizeFilter());
        }
    }
}