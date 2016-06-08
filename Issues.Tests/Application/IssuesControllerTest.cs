using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Issues.Application;
using Issues.Infrastructure;
using MyTested.WebApi;
using NSubstitute;
using NUnit.Framework;

namespace Issues.Tests
{
    class IssuesControllerTest
    {
        [Test]
        public void ListsIssues()
        {
            IIssuesRepository repository = Substitute.For<IIssuesRepository>();
            MyWebApi.Controller<IssuesController>().WithResolvedDependencyFor(repository).
                Calling(c => c.GetIssues()).ShouldReturn().StatusCode();
        }
    }
}
