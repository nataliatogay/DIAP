namespace brownshouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rfiIsRequired_adding : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InspectionRoles", "RFIIsRequired", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Tags", "Code", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tags", "Code", c => c.String(maxLength: 1));
            DropColumn("dbo.InspectionRoles", "RFIIsRequired");
        }
    }
}
