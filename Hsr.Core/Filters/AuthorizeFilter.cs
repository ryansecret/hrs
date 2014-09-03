using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hsr.Core.Filters
{
    public class AuthorizeFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            if (OutputCacheAttribute.IsChildActionCacheActive(filterContext))
                throw new InvalidOperationException(
                    "You cannot use [Authorize] attribute when a child action cache is active");

            if (!HasAccess(filterContext))
            {
                HandleUnauthorizedRequest(filterContext);
            }
        }

        public virtual bool HasAccess(AuthorizationContext filterContext)
        {
           var result= filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true)
                .OfType<AllowAnonymousAttribute>().Any();
            if (result)
            {
                return true;
            }
            if (filterContext.RequestContext.HttpContext.Session[CommonHelper.UserName] == null)
            {
                return false;
            }
            return false;
        }

        private void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}
