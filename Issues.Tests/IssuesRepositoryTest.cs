using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using FluentAssertions;
using Issues.Domain;
using Issues.Infrastructure;
using Issues.Migrations;
using NUnit.Framework;
using SQLite.CodeFirst;

namespace Issues.Tests
{
    public class IssuesRepositoryTest
    {
        [Test]
        public void TestMethod1()
        {
            var issuesTestContext = new IssuesTestContext();
            var issuesRepository = new IssuesRepository(issuesTestContext);

            var newIssue = NewIssue("EF sucks");
            issuesRepository.Add(newIssue);

            issuesTestContext.SaveChanges();

            var loaded = issuesTestContext.Issues.Find(1);
            
            loaded.ShouldBeEquivalentTo(newIssue);
        }

        private Issue NewIssue(string title)
        {
            return new Issue(1, title);
        }

        public class IssuesTestContext : IssuesContext
        {
 
            public IssuesTestContext() : base("IssuesTestContext")
            {
            }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                var sqliteConnectionInitializer = new SqliteDropCreateDatabaseAlways<IssuesTestContext>(modelBuilder);
                Database.SetInitializer(sqliteConnectionInitializer);
            }
        }
    }
}