namespace Issues.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "IsStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Issues", "IsStatus");
        }
    }
}
