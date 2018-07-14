namespace brownshouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class set_null_acceptances : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Requests", new[] { "IdAcceptanceSubcontractor" });
            DropIndex("dbo.Requests", new[] { "IdAcceptanceContractor" });
            AlterColumn("dbo.Requests", "IdAcceptanceSubcontractor", c => c.Int());
            AlterColumn("dbo.Requests", "IdAcceptanceContractor", c => c.Int());
            CreateIndex("dbo.Requests", "IdAcceptanceSubcontractor");
            CreateIndex("dbo.Requests", "IdAcceptanceContractor");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Requests", new[] { "IdAcceptanceContractor" });
            DropIndex("dbo.Requests", new[] { "IdAcceptanceSubcontractor" });
            AlterColumn("dbo.Requests", "IdAcceptanceContractor", c => c.Int(nullable: false));
            AlterColumn("dbo.Requests", "IdAcceptanceSubcontractor", c => c.Int(nullable: false));
            CreateIndex("dbo.Requests", "IdAcceptanceContractor");
            CreateIndex("dbo.Requests", "IdAcceptanceSubcontractor");
        }
    }
}
