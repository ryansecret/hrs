#region

using System.Web.Mvc;

#endregion



namespace Hsr.Core
{
    
    public class BaseController : Controller
    {
        protected virtual ActionResult InvokeHttp404()
        {
            // Call target Controller and pass the routeData.
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return new EmptyResult();
        }
    }
}