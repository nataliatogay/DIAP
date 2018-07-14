namespace brownshouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_users_to_requests : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Requests", "IdRaisedBy", "dbo.Users");
            DropForeignKey("dbo.Requests", "IdResponsibleUser", "dbo.Users");
            DropIndex("dbo.Requests", new[] { "IdRaisedBy" });
            DropIndex("dbo.Requests", new[] { "IdResponsibleUser" });
            AddColumn("dbo.Requests", "IdRaisedByEmployee", c => c.Int(nullable: false));
            AddColumn("dbo.Requests", "IdResponsibleEmployee", c => c.Int(nullable: false));
            CreateIndex("dbo.Requests", "IdRaisedByEmployee");
            CreateIndex("dbo.Requests", "IdResponsibleEmployee");
            AddForeignKey("dbo.Requests", "IdRaisedByEmployee", "dbo.Employees", "Id");
            AddForeignKey("dbo.Requests", "IdResponsibleEmployee", "dbo.Employees", "Id");
            DropColumn("dbo.Requests", "IdRaisedBy");
            DropColumn("dbo.Requests", "IdResponsibleUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requests", "IdResponsibleUser", c => c.Int(nullable: false));
            AddColumn("dbo.Requests", "IdRaisedBy", c => c.Int(nullable: false));
            DropForeignKey("dbo.Requests", "IdResponsibleEmployee", "dbo.Employees");
            DropForeignKey("dbo.Requests", "IdRaisedByEmployee", "dbo.Employees");
            DropIndex("dbo.Requests", new[] { "IdResponsibleEmployee" });
            DropIndex("dbo.Requests", new[] { "IdRaisedByEmployee" });
            DropColumn("dbo.Requests", "IdResponsibleEmployee");
            DropColumn("dbo.Requests", "IdRaisedByEmployee");
            CreateIndex("dbo.Requests", "IdResponsibleUser");
            CreateIndex("dbo.Requests", "IdRaisedBy");
            AddForeignKey("dbo.Requests", "IdResponsibleUser", "dbo.Users", "Id");
            AddForeignKey("dbo.Requests", "IdRaisedBy", "dbo.Users", "Id");
        }
    }
}
