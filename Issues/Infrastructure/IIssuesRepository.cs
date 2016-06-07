using System.Linq;
using Issues.Domain;

namespace Issues.Infrastructure
{
    public interface IIssuesRepository
    {
        IQueryable<Issue> All();
        Issue Add(Issue issue);
        Issue Load(int id);
        IQueryable<Issue> FindByStatus(Issue.IssueStatus status);
    }
}