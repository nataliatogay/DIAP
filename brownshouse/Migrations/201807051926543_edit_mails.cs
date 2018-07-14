namespace brownshouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_mails : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Mails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Mails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
