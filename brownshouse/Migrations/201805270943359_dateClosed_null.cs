namespace brownshouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateClosed_null : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Punches", "DateClosed", c => c.DateTime());
            DropColumn("dbo.Punches", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Punches", "Status", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Punches", "DateClosed", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
