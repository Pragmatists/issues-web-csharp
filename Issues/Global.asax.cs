using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Issues.Application;
using Issues.Domain;
using Issues.Infrastructure;

namespace Issues
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Database.SetInitializer(new IssuesDevInitializer());
            var controllerFactory = new IssuesControllerFactory();
            GlobalConfiguration.Configuration.Services.Replace(
                typeof (IHttpControllerActivator),
                controllerFactory);
        }
    }

    public class IssuesControllerFactory : IHttpControllerActivator
    {
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            if (controllerType == typeof (IssuesController))
            {
                return new IssuesController(new EFIssuesRepository(new IssuesContext()));
            }
            return null;
        }
    }

    public class IssuesDevInitializer : DropCreateDatabaseIfModelChanges<IssuesContext>
    {
        protected override void Seed(IssuesContext context)
        {
            new List<Issue>
            {
                new Issue(2, "Renaming issue")
            }.ForEach(i => context.Issues.Add(i));
        }
    }
}