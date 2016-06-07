using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
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
        private IssuesTestContext db;
        private IIssuesRepository repository;

        [SetUp]
        public void SetUp()
        {
            db = new IssuesTestContext();
            repository = new EFIssuesRepository(db);
            db.Database.ExecuteSqlCommand("DELETE FROM Issues");
        }

        [Test]
        public void LoadSavedIssue()
        {
            var newIssue = NewIssue("EF sucks");

            var newIssueId = repository.Add(newIssue).ID;
        
            var loaded = db.Issues.Find(newIssueId);
            loaded.ShouldBeEquivalentTo(newIssue);
        }

        [Test]
        public void FindsByStatus()
        {
            IssueExists(AnIssueInStatus(Issue.IssueStatus.OPEN));
            IssueExists(AnIssueInStatus(Issue.IssueStatus.CLOSED));

            IQueryable<Issue> found = repository.FindByStatus(Issue.IssueStatus.CLOSED);

            found.Should().HaveCount(1).And.Contain(x => x.Status == Issue.IssueStatus.CLOSED);
        }

        private void IssueExists(Issue anIssue)
        {
            repository.Add(anIssue);
        }

        private Issue AnIssueInStatus(Issue.IssueStatus issueStatus)
        {
            var issue = new Issue("default title");
            if (issueStatus == Issue.IssueStatus.CLOSED)
            {
                issue.FixedIn(AProductVersion());
                issue.Close();
            }
            return issue;
        }

        private ProductVersion AProductVersion()
        {
            return null;
        }

        [Test]
        public void FailsMeaningfullyWhenIssueDoesNotExist()
        {
            Action act = () => repository.Load(404);

            act.ShouldThrow<ArgumentException>().WithMessage("Issue does not exist");
        }

        private Issue NewIssue(string title)
        {
            return new Issue(title);
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
                base.OnModelCreating(modelBuilder);
            }
        }
    }
}