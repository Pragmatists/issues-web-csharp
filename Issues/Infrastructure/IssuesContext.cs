﻿using System.Data.Entity;

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

    public System.Data.Entity.DbSet<Issues.Domain.Issue> Issues { get; set; }
  }
}