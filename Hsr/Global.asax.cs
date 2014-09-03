using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FluentValidation;
using FluentValidation.Mvc;
using Hsr.Core;
using Hsr.Core.Infrastructure;
using Hsr.Core.Infrastructure.DependencyManagement;
using Hsr.Core.Log;
using Hsr.FluentCnResource;
 
using StackExchange.Profiling;
 
namespace Hsr
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("zh-CN");
            var dependencyResolver = new HsrDependencyResolver();
            DependencyResolver.SetResolver(dependencyResolver);

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
 
            ConfigureFluentValidation();
            EngineContext.Initialize(true);

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (CanPerformProfilingAction())
            {
                MiniProfiler.Start();
            }
           
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            if (CanPerformProfilingAction())
            {
                MiniProfiler.Stop();
            }
          
            //dispose registered resources
            //we do not register AutofacRequestLifetimeHttpModule as IHttpModule 
            //because it disposes resources before this Application_EndRequest method is called
            //and in this case the code in Application_EndRequest of Global.asax will throw an exception
            AutofacRequestLifetimeHttpModule.ContextEndRequest(sender, e);
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
 
            LogException(exception);
        }

        protected void LogException(Exception exc)
        {
            if (exc == null)
                return;

            
         
            var httpException = exc as HttpException;
            if (httpException != null && httpException.GetHttpCode() == 404)
                return;

            try
            {
              
                var logger = EngineContext.Current.Resolve<Log4Manager>();
             
                logger.Error(exc.Message, exc);
            }
            catch (Exception)
            {
               
            }
        }
        protected bool CanPerformProfilingAction()
        {
            //will not run in medium trust
            if (CommonHelper.GetTrustLevel() < AspNetHostingPermissionLevel.High)
                return false;

            return true;
        }

        protected void ConfigureFluentValidation()
        {

            ValidatorOptions.ResourceProviderType = typeof(ValidationResource);

          
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure; // ValidatorOptions.CascadeMode 默认值为：CascadeMode.Continue
            FluentValidationModelValidatorProvider.Configure();
      
        }
    }
}