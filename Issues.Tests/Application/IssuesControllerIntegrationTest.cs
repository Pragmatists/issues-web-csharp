using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
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
            //MyWebApi.IsUsing(GlobalConfiguration.Configuration);
            var controllerFactory = new IssuesControllerActivator();
            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator),
                controllerFactory);
            MyWebApi.IsUsing(GlobalConfiguration.Configuration);
            MyWebApi
                .Server()
                .Working() // working will instantiate new HTTP server with the global configuration
                .WithHttpRequestMessage(
                    request => request
                        .WithMethod(HttpMethod.Get)
                        .WithRequestUri("api/issues/1"))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.OK);
            MyWebApi.Server().Stops();
        }
    }
}
