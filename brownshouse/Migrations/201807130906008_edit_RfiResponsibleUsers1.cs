namespace brownshouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_RfiResponsibleUsers1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Requests", new[] { "ResponsibleContractor_Id" });
            DropColumn("dbo.Requests", "IdResponsibleContractor");
            DropColumn("dbo.Requests", "IdResponsibleOwner");
            DropColumn("dbo.Requests", "IdResponsibleThirdParty");
            RenameColumn(table: "dbo.Requests", name: "ResponsibleContractor_Id", newName: "IdResponsibleContractor");
            RenameColumn(table: "dbo.Requests", name: "ResponsibleOwner_Id", newName: "IdResponsibleOwner");
            RenameColumn(table: "dbo.Requests", name: "ResponsibleThirdParty_Id", newName: "IdResponsibleThirdParty");
            RenameIndex(table: "dbo.Requests", name: "IX_ResponsibleOwner_Id", newName: "IX_IdResponsibleOwner");
            RenameIndex(table: "dbo.Requests", name: "IX_ResponsibleThirdParty_Id", newName: "IX_IdResponsibleThirdParty");
            AlterColumn("dbo.Requests", "IdResponsibleContractor", c => c.Int(nullable: false));
            CreateIndex("dbo.Requests", "IdResponsibleContractor");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Requests", new[] { "IdResponsibleContractor" });
            AlterColumn("dbo.Requests", "IdResponsibleContractor", c => c.Int());
            RenameIndex(table: "dbo.Requests", name: "IX_IdResponsibleThirdParty", newName: "IX_ResponsibleThirdParty_Id");
            RenameIndex(table: "dbo.Requests", name: "IX_IdResponsibleOwner", newName: "IX_ResponsibleOwner_Id");
            RenameColumn(table: "dbo.Requests", name: "IdResponsibleThirdParty", newName: "ResponsibleThirdParty_Id");
            RenameColumn(table: "dbo.Requests", name: "IdResponsibleOwner", newName: "ResponsibleOwner_Id");
            RenameColumn(table: "dbo.Requests", name: "IdResponsibleContractor", newName: "ResponsibleContractor_Id");
            AddColumn("dbo.Requests", "IdResponsibleThirdParty", c => c.Int());
            AddColumn("dbo.Requests", "IdResponsibleOwner", c => c.Int());
            AddColumn("dbo.Requests", "IdResponsibleContractor", c => c.Int(nullable: false));
            CreateIndex("dbo.Requests", "ResponsibleContractor_Id");
        }
    }
}
