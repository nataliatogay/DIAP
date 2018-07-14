namespace brownshouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_idDiscipline_to_tags : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tags", "IdDiscipline", c => c.Int(nullable: false));
            CreateIndex("dbo.Tags", "IdDiscipline");
            AddForeignKey("dbo.Tags", "IdDiscipline", "dbo.Disciplines", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "IdDiscipline", "dbo.Disciplines");
            DropIndex("dbo.Tags", new[] { "IdDiscipline" });
            DropColumn("dbo.Tags", "IdDiscipline");
        }
    }
}
