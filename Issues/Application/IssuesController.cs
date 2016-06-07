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

        // GET: api/Issues
        public IQueryable<Issue> GetIssues()
        {
            return db.Issues;
        }

        // GET: api/Issues/5
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

        // PUT: api/Issues/5
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

        // POST: api/Issues
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

        // DELETE: api/Issues/5
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

/*
 * {"Message":"An error has occurred.","ExceptionMessage":"The 'ObjectContent`1'
 *  type failed to serialize the response body for content type 'application/json; charset=utf-8'.","ExceptionType":
 *  "System.InvalidOperationException","StackTrace":null,"InnerException":{"Message":"An error has occurred.","ExceptionMessage":
 *  "Model compatibility cannot be checked because the database does not contain model metadata. Model compatibility can only be c
 *  hecked for databases created using Code First or Code First Migrations.","ExceptionType":"System.NotSupportedException","StackTrace":"  
 *   w System.Data.Entity.Internal.ModelCompatibilityChecker.CompatibleWithModel(InternalContext internalContext, ModelHashCalculator modelHashCalculator, 
 *   Boolean throwIfNoMetadata, DatabaseExistenceState existenceState)\r\n   w System.Data.Entity.Internal.InternalContext.CompatibleWithModel(Boolean throwIfNoMetadata
 *   , DatabaseExistenceState existenceState)\r\n   w System.Data.Entity.DropCreateDatabaseIfModelChanges`1.InitializeDatabase(TContext context)\r\n   w System.Data.Entity.Internal.InternalContext.<>c__DisplayClassf`1.<CreateInitializationAction>b__e()\r\n   w System.Data.Entity.Internal.InternalContext.PerformInitializationAction(Action action)\r\n   w System.Data.Entity.Internal.InternalContext.PerformDatabaseInitialization()\r\n   w System.Data.Entity.Internal.LazyInternalContext.<InitializeDatabase>b__4(InternalContext c)\r\n   w System.Data.Entity.Internal.RetryAction`1.PerformAction(TInput input)\r\n   w System.Data.Entity.Internal.LazyInternalContext.InitializeDatabaseAction(Action`1 action)\r\n   w System.Data.Entity.Internal.LazyInternalContext.InitializeDatabase()\r\n   w System.Data.Entity.Internal.InternalContext.GetEntitySetAndBaseTypeForType(Type entityType)\r\n   w System.Data.Entity.Internal.Linq.InternalSet`1.Initialize()\r\n   w System.Data.Entity.Internal.Linq.InternalSet`1.GetEnumerator()\r\n   w System.Data.Entity.Infrastructure.DbQuery`1.System.Collections.IEnumerable.GetEnumerator()\r\n   w Newtonsoft.Json.Serialization.JsonSerializerInternalWriter.SerializeList(JsonWriter writer, IEnumerable values, JsonArrayContract contract, JsonProperty member, JsonContainerContract collectionContract, JsonProperty containerProperty)\r\n   w Newtonsoft.Json.Serialization.JsonSerializerInternalWriter.SerializeValue(JsonWriter writer, Object value, JsonContract valueContract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerProperty)\r\n   w Newtonsoft.Json.Serialization.JsonSerializerInternalWriter.Serialize(JsonWriter jsonWriter, Object value, Type objectType)\r\n   w Newtonsoft.Json.JsonSerializer.SerializeInternal(JsonWriter jsonWriter, Object value, Type objectType)\r\n   w System.Net.Http.Formatting.BaseJsonMediaTypeFormatter.WriteToStream(Type type, Object value, Stream writeStream, Encoding effectiveEncoding)\r\n   w System.Net.Http.Formatting.JsonMediaTypeFormatter.WriteToStream(Type type, Object value, Stream writeStream, Encoding effectiveEncoding)\r\n   w System.Net.Http.Formatting.BaseJsonMediaTypeFormatter.WriteToStream(Type type, Object value, Stream writeStream, HttpContent content)\r\n   w System.Net.Http.Formatting.BaseJsonMediaTypeFormatter.WriteToStreamAsync(Type type, Object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)\r\n--- Koniec śladu stosu z poprzedniej lokalizacji, w której wystąpił wyjątek ---\r\n   w System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   w System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   w System.Web.Http.WebHost.HttpControllerHandler.<WriteBufferedResponseContentAsync>d__1b.MoveNext()"}}

 * */