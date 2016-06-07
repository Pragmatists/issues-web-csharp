using System.Data.Entity;
using Issues.Domain;

namespace Issues.Infrastructure
{
    public class IssuesContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public IssuesContext() : base("name=IssuesContext")
        {
        }

        protected IssuesContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.ComplexType<Issue.IssueStatus>();
            modelBuilder.Types<Issue>().Configure(c=>c.Property(x=>x.Status.Name).HasColumnName("IsStatus"));
        }

        public DbSet<Domain.Issue> Issues { get; set; }
  }
}
