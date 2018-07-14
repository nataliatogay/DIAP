namespace brownshouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_email_employee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Email");
        }
    }
}
