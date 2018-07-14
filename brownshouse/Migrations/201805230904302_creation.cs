namespace brownshouse.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcceptanceResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Result = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        IdProject = c.Int(nullable: false),
                        IdUnit = c.Int(nullable: false),
                        IdSubsyst = c.Int(nullable: false),
                        IdTestPlan = c.Int(nullable: false),
                        DateOpen = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IdAcceptanceSubcontractor = c.Int(nullable: false),
                        IdAcceptanceContractor = c.Int(nullable: false),
                        IdAcceptanceOwner = c.Int(),
                        IdAcceptanceThirdParty = c.Int(),
                        DateToDo = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IdStatus = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InspectionTestPlans", t => t.IdTestPlan)
                .ForeignKey("dbo.Subsysts", t => t.IdSubsyst)
                .ForeignKey("dbo.Units", t => t.IdUnit)
                .ForeignKey("dbo.Projects", t => t.IdProject)
                .ForeignKey("dbo.AcceptanceResults", t => t.IdAcceptanceContractor)
                .ForeignKey("dbo.AcceptanceResults", t => t.IdAcceptanceOwner)
                .ForeignKey("dbo.AcceptanceResults", t => t.IdStatus)
                .ForeignKey("dbo.AcceptanceResults", t => t.IdAcceptanceSubcontractor)
                .ForeignKey("dbo.AcceptanceResults", t => t.IdAcceptanceThirdParty)
                .Index(t => t.IdProject)
                .Index(t => t.IdUnit)
                .Index(t => t.IdSubsyst)
                .Index(t => t.IdTestPlan)
                .Index(t => t.IdAcceptanceSubcontractor)
                .Index(t => t.IdAcceptanceContractor)
                .Index(t => t.IdAcceptanceOwner)
                .Index(t => t.IdAcceptanceThirdParty)
                .Index(t => t.IdStatus);
            
            CreateTable(
                "dbo.InspectionTestPlans",
                c => new
                    {
                        IdSubactivity = c.Int(nullable: false),
                        IdForm = c.Int(nullable: false),
                        IdSubcontractorRole = c.Int(nullable: false),
                        IdContractorRole = c.Int(nullable: false),
                        IdOwnerRole = c.Int(),
                        IdThirdPartRole = c.Int(),
                    })
                .PrimaryKey(t => t.IdSubactivity)
                .ForeignKey("dbo.InspectionRoles", t => t.IdContractorRole)
                .ForeignKey("dbo.InspectionRoles", t => t.IdOwnerRole)
                .ForeignKey("dbo.InspectionRoles", t => t.IdSubcontractorRole)
                .ForeignKey("dbo.InspectionRoles", t => t.IdThirdPartRole)
                .ForeignKey("dbo.Forms", t => t.IdForm)
                .ForeignKey("dbo.Subactivities", t => t.IdSubactivity)
                .Index(t => t.IdSubactivity)
                .Index(t => t.IdForm)
                .Index(t => t.IdSubcontractorRole)
                .Index(t => t.IdContractorRole)
                .Index(t => t.IdOwnerRole)
                .Index(t => t.IdThirdPartRole);
            
            CreateTable(
                "dbo.InspectionRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Forms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        FilePath = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subactivities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Number = c.Double(nullable: false),
                        IdActivity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activities", t => t.IdActivity)
                .Index(t => t.IdActivity);
            
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Number = c.Int(nullable: false),
                        IdWork = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Works", t => t.IdWork)
                .Index(t => t.IdWork);
            
            CreateTable(
                "dbo.Works",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        IdDiscipline = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Disciplines", t => t.IdDiscipline)
                .Index(t => t.IdDiscipline);
            
            CreateTable(
                "dbo.Disciplines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdOrganization = c.Int(nullable: false),
                        IdDiscipline = c.Int(nullable: false),
                        IdPosition = c.Int(nullable: false),
                        Abbreviation = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.IdOrganization)
                .ForeignKey("dbo.Positions", t => t.IdPosition)
                .ForeignKey("dbo.Disciplines", t => t.IdDiscipline)
                .Index(t => t.IdOrganization)
                .Index(t => t.IdDiscipline)
                .Index(t => t.IdPosition);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        IdOrganizationRole = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrganizationRoles", t => t.IdOrganizationRole)
                .Index(t => t.IdOrganizationRole);
            
            CreateTable(
                "dbo.OrganizationRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Role = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Punches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdTag = c.Int(nullable: false),
                        IdPriority = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        Status = c.Boolean(nullable: false),
                        DateOpened = c.DateTime(nullable: false),
                        DateClosed = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IdRaisedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PunchPriorities", t => t.IdPriority)
                .ForeignKey("dbo.Tags", t => t.IdTag)
                .ForeignKey("dbo.Users", t => t.IdRaisedBy)
                .Index(t => t.IdTag)
                .Index(t => t.IdPriority)
                .Index(t => t.IdRaisedBy);
            
            CreateTable(
                "dbo.PunchPriorities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Priority = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 1),
                        Title = c.String(),
                        IdSubsyst = c.Int(nullable: false),
                        IdUnit = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subsysts", t => t.IdSubsyst)
                .ForeignKey("dbo.Units", t => t.IdUnit)
                .Index(t => t.IdSubsyst)
                .Index(t => t.IdUnit);
            
            CreateTable(
                "dbo.Subsysts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        IdSyst = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Systs", t => t.IdSyst)
                .Index(t => t.IdSyst);
            
            CreateTable(
                "dbo.Systs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagsRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdTag = c.Int(nullable: false),
                        IdRequest = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tags", t => t.IdTag)
                .ForeignKey("dbo.Requests", t => t.IdRequest)
                .Index(t => t.IdTag)
                .Index(t => t.IdRequest);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "IdAcceptanceThirdParty", "dbo.AcceptanceResults");
            DropForeignKey("dbo.Requests", "IdAcceptanceSubcontractor", "dbo.AcceptanceResults");
            DropForeignKey("dbo.Requests", "IdStatus", "dbo.AcceptanceResults");
            DropForeignKey("dbo.Requests", "IdAcceptanceOwner", "dbo.AcceptanceResults");
            DropForeignKey("dbo.Requests", "IdAcceptanceContractor", "dbo.AcceptanceResults");
            DropForeignKey("dbo.TagsRequests", "IdRequest", "dbo.Requests");
            DropForeignKey("dbo.Requests", "IdProject", "dbo.Projects");
            DropForeignKey("dbo.InspectionTestPlans", "IdSubactivity", "dbo.Subactivities");
            DropForeignKey("dbo.Works", "IdDiscipline", "dbo.Disciplines");
            DropForeignKey("dbo.Users", "IdDiscipline", "dbo.Disciplines");
            DropForeignKey("dbo.Punches", "IdRaisedBy", "dbo.Users");
            DropForeignKey("dbo.Tags", "IdUnit", "dbo.Units");
            DropForeignKey("dbo.Requests", "IdUnit", "dbo.Units");
            DropForeignKey("dbo.TagsRequests", "IdTag", "dbo.Tags");
            DropForeignKey("dbo.Tags", "IdSubsyst", "dbo.Subsysts");
            DropForeignKey("dbo.Subsysts", "IdSyst", "dbo.Systs");
            DropForeignKey("dbo.Requests", "IdSubsyst", "dbo.Subsysts");
            DropForeignKey("dbo.Punches", "IdTag", "dbo.Tags");
            DropForeignKey("dbo.Punches", "IdPriority", "dbo.PunchPriorities");
            DropForeignKey("dbo.Users", "IdPosition", "dbo.Positions");
            DropForeignKey("dbo.Users", "IdOrganization", "dbo.Organizations");
            DropForeignKey("dbo.Organizations", "IdOrganizationRole", "dbo.OrganizationRoles");
            DropForeignKey("dbo.Activities", "IdWork", "dbo.Works");
            DropForeignKey("dbo.Subactivities", "IdActivity", "dbo.Activities");
            DropForeignKey("dbo.Requests", "IdTestPlan", "dbo.InspectionTestPlans");
            DropForeignKey("dbo.InspectionTestPlans", "IdForm", "dbo.Forms");
            DropForeignKey("dbo.InspectionTestPlans", "IdThirdPartRole", "dbo.InspectionRoles");
            DropForeignKey("dbo.InspectionTestPlans", "IdSubcontractorRole", "dbo.InspectionRoles");
            DropForeignKey("dbo.InspectionTestPlans", "IdOwnerRole", "dbo.InspectionRoles");
            DropForeignKey("dbo.InspectionTestPlans", "IdContractorRole", "dbo.InspectionRoles");
            DropIndex("dbo.TagsRequests", new[] { "IdRequest" });
            DropIndex("dbo.TagsRequests", new[] { "IdTag" });
            DropIndex("dbo.Subsysts", new[] { "IdSyst" });
            DropIndex("dbo.Tags", new[] { "IdUnit" });
            DropIndex("dbo.Tags", new[] { "IdSubsyst" });
            DropIndex("dbo.Punches", new[] { "IdRaisedBy" });
            DropIndex("dbo.Punches", new[] { "IdPriority" });
            DropIndex("dbo.Punches", new[] { "IdTag" });
            DropIndex("dbo.Organizations", new[] { "IdOrganizationRole" });
            DropIndex("dbo.Users", new[] { "IdPosition" });
            DropIndex("dbo.Users", new[] { "IdDiscipline" });
            DropIndex("dbo.Users", new[] { "IdOrganization" });
            DropIndex("dbo.Works", new[] { "IdDiscipline" });
            DropIndex("dbo.Activities", new[] { "IdWork" });
            DropIndex("dbo.Subactivities", new[] { "IdActivity" });
            DropIndex("dbo.InspectionTestPlans", new[] { "IdThirdPartRole" });
            DropIndex("dbo.InspectionTestPlans", new[] { "IdOwnerRole" });
            DropIndex("dbo.InspectionTestPlans", new[] { "IdContractorRole" });
            DropIndex("dbo.InspectionTestPlans", new[] { "IdSubcontractorRole" });
            DropIndex("dbo.InspectionTestPlans", new[] { "IdForm" });
            DropIndex("dbo.InspectionTestPlans", new[] { "IdSubactivity" });
            DropIndex("dbo.Requests", new[] { "IdStatus" });
            DropIndex("dbo.Requests", new[] { "IdAcceptanceThirdParty" });
            DropIndex("dbo.Requests", new[] { "IdAcceptanceOwner" });
            DropIndex("dbo.Requests", new[] { "IdAcceptanceContractor" });
            DropIndex("dbo.Requests", new[] { "IdAcceptanceSubcontractor" });
            DropIndex("dbo.Requests", new[] { "IdTestPlan" });
            DropIndex("dbo.Requests", new[] { "IdSubsyst" });
            DropIndex("dbo.Requests", new[] { "IdUnit" });
            DropIndex("dbo.Requests", new[] { "IdProject" });
            DropTable("dbo.Projects");
            DropTable("dbo.Units");
            DropTable("dbo.TagsRequests");
            DropTable("dbo.Systs");
            DropTable("dbo.Subsysts");
            DropTable("dbo.Tags");
            DropTable("dbo.PunchPriorities");
            DropTable("dbo.Punches");
            DropTable("dbo.Positions");
            DropTable("dbo.OrganizationRoles");
            DropTable("dbo.Organizations");
            DropTable("dbo.Users");
            DropTable("dbo.Disciplines");
            DropTable("dbo.Works");
            DropTable("dbo.Activities");
            DropTable("dbo.Subactivities");
            DropTable("dbo.Forms");
            DropTable("dbo.InspectionRoles");
            DropTable("dbo.InspectionTestPlans");
            DropTable("dbo.Requests");
            DropTable("dbo.AcceptanceResults");
        }
    }
}
