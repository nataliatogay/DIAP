namespace brownshouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_RfiResponsibleUsers : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Requests", name: "IdResponsibleEmployee", newName: "IdResponsibleSubcontractor");
            RenameIndex(table: "dbo.Requests", name: "IX_IdResponsibleEmployee", newName: "IX_IdResponsibleSubcontractor");
            AddColumn("dbo.Requests", "IdResponsibleContractor", c => c.Int(nullable: false));
            AddColumn("dbo.Requests", "IdResponsibleOwner", c => c.Int());
            AddColumn("dbo.Requests", "IdResponsibleThirdParty", c => c.Int());
            AddColumn("dbo.Requests", "ResponsibleContractor_Id", c => c.Int());
            AddColumn("dbo.Requests", "ResponsibleOwner_Id", c => c.Int());
            AddColumn("dbo.Requests", "ResponsibleThirdParty_Id", c => c.Int());
            CreateIndex("dbo.Requests", "ResponsibleContractor_Id");
            CreateIndex("dbo.Requests", "ResponsibleOwner_Id");
            CreateIndex("dbo.Requests", "ResponsibleThirdParty_Id");
            AddForeignKey("dbo.Requests", "ResponsibleContractor_Id", "dbo.Employees", "Id");
            AddForeignKey("dbo.Requests", "ResponsibleOwner_Id", "dbo.Employees", "Id");
            AddForeignKey("dbo.Requests", "ResponsibleThirdParty_Id", "dbo.Employees", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "ResponsibleThirdParty_Id", "dbo.Employees");
            DropForeignKey("dbo.Requests", "ResponsibleOwner_Id", "dbo.Employees");
            DropForeignKey("dbo.Requests", "ResponsibleContractor_Id", "dbo.Employees");
            DropIndex("dbo.Requests", new[] { "ResponsibleThirdParty_Id" });
            DropIndex("dbo.Requests", new[] { "ResponsibleOwner_Id" });
            DropIndex("dbo.Requests", new[] { "ResponsibleContractor_Id" });
            DropColumn("dbo.Requests", "ResponsibleThirdParty_Id");
            DropColumn("dbo.Requests", "ResponsibleOwner_Id");
            DropColumn("dbo.Requests", "ResponsibleContractor_Id");
            DropColumn("dbo.Requests", "IdResponsibleThirdParty");
            DropColumn("dbo.Requests", "IdResponsibleOwner");
            DropColumn("dbo.Requests", "IdResponsibleContractor");
            RenameIndex(table: "dbo.Requests", name: "IX_IdResponsibleSubcontractor", newName: "IX_IdResponsibleEmployee");
            RenameColumn(table: "dbo.Requests", name: "IdResponsibleSubcontractor", newName: "IdResponsibleEmployee");
        }
    }
}
