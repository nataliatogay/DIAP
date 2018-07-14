namespace brownshouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_programmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProgramMails", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProgramMails", "IsActive");
        }
    }
}
