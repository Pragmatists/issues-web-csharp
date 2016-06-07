using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Issues.Domain;
using Issues.Infrastructure;

namespace Issues.Application
{
    public class IssuesController : ApiController
    {
        private readonly IssuesContext db = new IssuesContext();
        private readonly IIssuesRepository issuesRepository;

        public IssuesController(IIssuesRepository issuesRepository)
        {
            this.issuesRepository = issuesRepository;
        }

        // GET: api/IssuesRepository
        public IQueryable<Issue> GetIssues()
        {
            return issuesRepository.All();
        }

        // GET: api/IssuesRepository/5
        [ResponseType(typeof (Issue))]
        public IHttpActionResult GetIssue(int id)
        {
            var issue = db.Issues.Find(id);
            if (issue == null)
            {
                return NotFound();
            }

            return Ok(issue);
        }

        // PUT: api/IssuesRepository/5
        [ResponseType(typeof (void))]
        public IHttpActionResult PutIssue(int id, Issue issue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != issue.ID)
            {
                return BadRequest();
            }

            db.Entry(issue).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IssueExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/IssuesRepository
        [ResponseType(typeof (Issue))]
        public IHttpActionResult PostIssue(Issue issue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Issues.Add(issue);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new {id = issue.ID}, issue);
        }

        // DELETE: api/IssuesRepository/5
        [ResponseType(typeof (Issue))]
        public IHttpActionResult DeleteIssue(int id)
        {
            var issue = db.Issues.Find(id);
            if (issue == null)
            {
                return NotFound();
            }

            db.Issues.Remove(issue);
            db.SaveChanges();

            return Ok(issue);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IssueExists(int id)
        {
            return db.Issues.Count(e => e.ID == id) > 0;
        }
    }
}