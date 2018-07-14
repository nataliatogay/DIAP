namespace brownshouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_subactivity_number : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subactivities", "Number", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subactivities", "Number", c => c.Double(nullable: false));
        }
    }
}
