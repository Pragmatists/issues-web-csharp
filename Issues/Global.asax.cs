using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Issues.Domain;
using Issues.Infrastructure;

namespace Issues
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Database.SetInitializer(new IssuesDevInitializer());
        
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
