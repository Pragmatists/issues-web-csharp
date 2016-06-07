using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Issues.Domain;
using NUnit.Framework;

namespace Issues.Tests.Domain
{
    class IssueTest
    {
        [Test]
        public void NewIssueIsOpen()
        {
            new Issue().Status.Should().Be(Issue.IssueStatus.OPEN);
        }
    }
}
