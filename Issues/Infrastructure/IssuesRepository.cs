using System.Linq;
using Issues.Domain;

namespace Issues.Infrastructure
{
    public class IssuesRepository : IIssuesRepository
    {
        private IssuesContext db;

        public IssuesRepository(IssuesContext db)
        {
            this.db = db;
        }

        public IQueryable<Issue> All()
        {
            return db.Issues;
        }

        public Issue Add(Issue issue)
        {
            return db.Issues.Add(issue);
        }
    }
}