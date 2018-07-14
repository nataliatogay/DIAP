namespace brownshouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_raisedby_request : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Punches", "IdRaisedBy", "dbo.Users");
            DropIndex("dbo.Punches", new[] { "IdRaisedBy" });
            AddColumn("dbo.Punches", "IdRaisedByEmployee", c => c.Int(nullable: false));
            CreateIndex("dbo.Punches", "IdRaisedByEmployee");
            AddForeignKey("dbo.Punches", "IdRaisedByEmployee", "dbo.Employees", "Id");
            DropColumn("dbo.Punches", "IdRaisedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Punches", "IdRaisedBy", c => c.Int(nullable: false));
            DropForeignKey("dbo.Punches", "IdRaisedByEmployee", "dbo.Employees");
            DropIndex("dbo.Punches", new[] { "IdRaisedByEmployee" });
            DropColumn("dbo.Punches", "IdRaisedByEmployee");
            CreateIndex("dbo.Punches", "IdRaisedBy");
            AddForeignKey("dbo.Punches", "IdRaisedBy", "dbo.Users", "Id");
        }
    }
}
