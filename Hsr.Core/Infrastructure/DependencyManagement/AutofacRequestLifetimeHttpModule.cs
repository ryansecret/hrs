﻿#region

using System;
using System.Web;
using Autofac;

#endregion

namespace Hsr.Core.Infrastructure.DependencyManagement
{
    /// <summary>
    ///     An <see cref="IHttpModule" /> and <see cref="ILifetimeScopeProvider" /> implementation
    ///     that creates a nested lifetime scope for each HTTP request.
    /// </summary>
    public class AutofacRequestLifetimeHttpModule : IHttpModule
    {
        /// <summary>
        ///     Tag used to identify registrations that are scoped to the HTTP request level.
        /// </summary>
        //in the previous versions of Autofac (for MVC3) it was set to "httpRequest"
        public static readonly object HttpRequestTag = "AutofacWebRequest";

        private static ILifetimeScope LifetimeScope
        {
            get { return (ILifetimeScope) HttpContext.Current.Items[typeof (ILifetimeScope)]; }
            set { HttpContext.Current.Items[typeof (ILifetimeScope)] = value; }
        }

        /// <summary>
        ///     Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">
        ///     An <see cref="T:System.Web.HttpApplication" /> that provides access to the
        ///     methods, properties, and events common to all application objects within an ASP.NET application
        /// </param>
        public void Init(HttpApplication context)
        {
            context.EndRequest += ContextEndRequest;
        }

        /// <summary>
        ///     Disposes of the resources (other than memory) used by the module that implements
        ///     <see cref="T:System.Web.IHttpModule" />.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        ///     Gets a nested lifetime scope that services can be resolved from.
        /// </summary>
        /// <param name="container">The parent container.</param>
        /// <param name="configurationAction">
        ///     Action on a <see cref="ContainerBuilder" />
        ///     that adds component registations visible only in nested lifetime scopes.
        /// </param>
        /// <returns>A new or existing nested lifetime scope.</returns>
        public static ILifetimeScope GetLifetimeScope(ILifetimeScope container,
            Action<ContainerBuilder> configurationAction)
        {
            //little hack here to get dependencies when HttpContext is not available
            if (HttpContext.Current != null)
            {
                return LifetimeScope ?? (LifetimeScope = InitializeLifetimeScope(configurationAction, container));
            }
            //throw new InvalidOperationException("HttpContextNotAvailable");
            return InitializeLifetimeScope(configurationAction, container);
        }

        public static void ContextEndRequest(object sender, EventArgs e)
        {
            try
            {
                //try-catch here
                //For more info see the following forum topic - http://www.nopcommerce.com/boards/t/22456/30-changeset-3db3868edcf2-loaderlock-was-detected.aspx

                ILifetimeScope lifetimeScope = LifetimeScope;
                if (lifetimeScope != null)
                    lifetimeScope.Dispose();
            }
            catch (Exception exc)
            {
            }
        }

        private static ILifetimeScope InitializeLifetimeScope(Action<ContainerBuilder> configurationAction,
            ILifetimeScope container)
        {
            return (configurationAction == null)
                ? container.BeginLifetimeScope(HttpRequestTag)
                : container.BeginLifetimeScope(HttpRequestTag, configurationAction);
        }
    }
}