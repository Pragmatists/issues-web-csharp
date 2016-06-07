using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using Issues.Domain;
using Issues.Infrastructure;
using Issues.Migrations;
using NUnit.Framework;
using SQLite.CodeFirst;

namespace Issues.Tests
{
    public class IssuesRepoTest
    {
        [Test]
        public void TestMethod1()
        {
            var issuesTestContext = new IssuesTestContext();
            issuesTestContext.Issues.Add(new Issue(1, "EF sucks"));
            issuesTestContext.SaveChanges();
            var issue = issuesTestContext.Issues.Find(1);
            Assert.That(issue.Title, Is.EqualTo("EF sucks"));
        }

        public class IssuesTestContext : DbContext
        {
 
            public IssuesTestContext() : base("IssuesTestContext")
            {
            }

            public DbSet<Issue> Issues { get; set; }
            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                var sqliteConnectionInitializer = new SqliteDropCreateDatabaseAlways<IssuesTestContext>(modelBuilder);
                Database.SetInitializer(sqliteConnectionInitializer);
            }
        }
    }
}