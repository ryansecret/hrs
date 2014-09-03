using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using Hsr.Core;
using Hsr.Core.Cache;
using Hsr.Core.Infrastructure;
using Hsr.Core.Infrastructure.DependencyManagement;
using Hsr.Data;
using Hsr.Data.Interface;

namespace Hsr
{
    public class HsrRegister : IDependencyRegister
    {
        public int Order { get { return 0; }}
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
              builder.Register(c => new HttpContextWrapper(HttpContext.Current) as HttpContextBase)
                .As<HttpContextBase>()
                .InstancePerHttpRequest();

            builder.Register<IDbContext>(c=>new PublicDataContext()).InstancePerHttpRequest();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerHttpRequest();
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().SingleInstance();
            builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>().Named<ICacheManager>("nop_cache_per_request").InstancePerHttpRequest();
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerHttpRequest();
        }
 
    }
}