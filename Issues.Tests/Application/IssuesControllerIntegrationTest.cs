using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Issues.Application;
using Issues.Infrastructure;
using MyTested.WebApi;
using NUnit.Framework;

namespace Issues.Tests.Application
{
    class IssuesControllerIntegrationTest
    {
        [Test]
        public void GetAnIssue()
        {
            // starts HTTP server with global configuration
            // set with MyWebApi.IsUsing
            // * the server is disposed after the test
            // * HTTP request can be set just like in the controller unit tests
            // * HTTP response can be tested just like in the controller unit tests
            //MyWebApi.IsUsing(GlobalConfiguration.Configuration)
            
            var config = new HttpConfiguration { IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always };

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "TestRoute",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            var controllerFactory = new TestIssuesControllerActivator();
            config.Services.Replace(
                typeof(IHttpControllerActivator),
                controllerFactory);

            MyWebApi.IsUsing(config);
            MyWebApi
                .Server()
                .Working() // working will instantiate new HTTP server with the global configuration
                .WithHttpRequestMessage(
                    request => request
                        .WithMethod(HttpMethod.Get)
                        .WithRequestUri("api/issues"))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.OK);
        }

        public class TestIssuesControllerActivator : IHttpControllerActivator
        {
            public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor,
                Type controllerType)
            {
                if (controllerType == typeof(IssuesController))
                {
                    return new IssuesController(new EFIssuesRepository(new IssuesRepositoryTest.IssuesTestContext()));
                }
                return null;
            }
        }
    }
}
