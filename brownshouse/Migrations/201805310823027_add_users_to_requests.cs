namespace brownshouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_users_to_requests : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "IdRaisedBy", c => c.Int(nullable: false));
            AddColumn("dbo.Requests", "IdResponsibleUser", c => c.Int(nullable: false));
            CreateIndex("dbo.Requests", "IdRaisedBy");
            CreateIndex("dbo.Requests", "IdResponsibleUser");
            AddForeignKey("dbo.Requests", "IdRaisedBy", "dbo.Users", "Id");
            AddForeignKey("dbo.Requests", "IdResponsibleUser", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "IdResponsibleUser", "dbo.Users");
            DropForeignKey("dbo.Requests", "IdRaisedBy", "dbo.Users");
            DropIndex("dbo.Requests", new[] { "IdResponsibleUser" });
            DropIndex("dbo.Requests", new[] { "IdRaisedBy" });
            DropColumn("dbo.Requests", "IdResponsibleUser");
            DropColumn("dbo.Requests", "IdRaisedBy");
        }
    }
}
