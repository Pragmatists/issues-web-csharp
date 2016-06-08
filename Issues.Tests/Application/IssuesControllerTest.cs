using System;
using System.Web.Http.Results;
using FluentAssertions;
using Issues.Application;
using Issues.Domain;
using Issues.Infrastructure;
using MyTested.WebApi;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace Issues.Tests
{
    internal class IssuesControllerTest
    {
        private IIssuesRepository repository;
        private IssuesController controller;

        [SetUp]
        public void SetUp()
        {
            repository = Substitute.For<IIssuesRepository>();
            controller = new IssuesController(repository);
        }
        [Test]
        public void GetsAnIssue()
        {
            repository.Load(1).Returns(new Issue("an issue"));
            var httpActionResult = controller.GetIssue(1) as OkNegotiatedContentResult<Issue>;
            httpActionResult.Content.Title.Should().Be("an issue");
        }

        [Test]
        public void CannotFindAnIssue()
        {
            repository.Load(1).Throws<ArgumentException>();
            var httpActionResult = controller.GetIssue(1);
            httpActionResult.Should().BeAssignableTo<NotFoundResult>();
        }

        [Test]
        public void GetsAnIssue_MyWebApi()
        {
            repository.Load(1).Returns(new Issue("an issue"));
            MyWebApi.Controller<IssuesController>().WithResolvedDependencyFor(repository).
                Calling(c => c.GetIssue(1))
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<Issue>()
                .Passing(m => { m.Title.Should().Be("an issue"); });
        }
    }
}