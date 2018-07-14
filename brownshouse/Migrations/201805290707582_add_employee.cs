namespace brownshouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_employee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        IdUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.IdUser)
                .Index(t => t.IdUser);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "IdUser", "dbo.Users");
            DropIndex("dbo.Employees", new[] { "IdUser" });
            DropTable("dbo.Employees");
        }
    }
}
