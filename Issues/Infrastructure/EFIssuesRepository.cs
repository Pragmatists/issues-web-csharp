using System;
using System.Linq;
using Issues.Domain;

namespace Issues.Infrastructure
{
    public class EFIssuesRepository : IIssuesRepository
    {
        private IssuesContext db;

        public EFIssuesRepository(IssuesContext db)
        {
            this.db = db;
        }

        public IQueryable<Issue> All()
        {
            return db.Issues;
        }

        public Issue Add(Issue issue)
        {
            var added = db.Issues.Add(issue);
            db.SaveChanges();
            return added;
        }

        public Issue Load(int id)
        {
            var issue = db.Issues.Find(id);
            if (issue == null) throw new ArgumentException("issue does not exist");
            return issue;
        }

        public IQueryable<Issue> FindByStatus(Issue.IssueStatus status)
        {
            return All().Where(x => x.Status.Name.Equals(status.Name));
        }
    }
}