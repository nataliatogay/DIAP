using brownshouse.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.EntityFramework
{
    public class BrownsContext : DbContext
    {
        public BrownsContext() : base("BrownsDbase")
        {
            Database.SetInitializer<BrownsContext>(
                    new CreateDatabaseIfNotExists<BrownsContext>()
                );
        }

        public virtual DbSet<AcceptanceResult> AcceptanceResults { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Discipline> Disciplines { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Form> Forms { get; set; }
        public virtual DbSet<InspectionRole> InspectionRoles { get; set; }
        public virtual DbSet<InspectionTestPlan> InspectionTestPlans { get; set; }
        public virtual DbSet<OrganizationRole> OrganizationRoles { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<ProgramMail> ProgramMails { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Punch> Punches { get; set; }
        public virtual DbSet<PunchPriority> PunchPriorities { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Subactivity> Subactivities { get; set; }
        public virtual DbSet<Subsyst> Subsysts { get; set; }
        public virtual DbSet<Syst> Systs { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<TagsRequest> TagsRequests { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Work> Works { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcceptanceResult>()
                .HasMany(e => e.SubcontractorRequests)
                .WithOptional(e => e.AcceptanceSubcontractor)
                .HasForeignKey(e => e.IdAcceptanceSubcontractor);

            modelBuilder.Entity<AcceptanceResult>()
                .HasMany(e => e.ContractorRequests)
                .WithOptional(e => e.AcceptanceContractor)
                .HasForeignKey(e => e.IdAcceptanceContractor);

            modelBuilder.Entity<AcceptanceResult>()
                .HasMany(e => e.OwnerRequests)
                .WithOptional(e => e.AcceptanceOwner)
                .HasForeignKey(e => e.IdAcceptanceOwner);

            modelBuilder.Entity<AcceptanceResult>()
                .HasMany(e => e.ThirdPartRequests)
                .WithOptional(e => e.AcceptanceThirdParty)
                .HasForeignKey(e => e.IdAcceptanceThirdParty);

            modelBuilder.Entity<AcceptanceResult>()
                .HasMany(e => e.StatusRequests)
                .WithOptional(e => e.Status)
                .HasForeignKey(e => e.IdStatus);

            modelBuilder.Entity<Employee>()
               .HasMany(e => e.OwnerRequests)
               .WithOptional(e => e.ResponsibleOwner)
               .HasForeignKey(e => e.IdResponsibleOwner);

            modelBuilder.Entity<Employee>()
              .HasMany(e => e.ThirdPartRequests)
              .WithOptional(e => e.ResponsibleThirdParty)
              .HasForeignKey(e => e.IdResponsibleThirdParty);


            modelBuilder.Entity<Activity>()
                .HasMany(e => e.Subactivities)
                .WithRequired(e => e.Activity)
                .HasForeignKey(e => e.IdActivity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Discipline>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Discipline)
                .HasForeignKey(e => e.IdDiscipline)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Discipline>()
                .HasMany(e => e.Works)
                .WithRequired(e => e.Discipline)
                .HasForeignKey(e => e.IdDiscipline)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Discipline>()
                .HasMany(e => e.Tags)
                .WithRequired(e => e.Discipline)
                .HasForeignKey(e => e.IdDiscipline)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Form>()
                .HasMany(e => e.InspectionTestPlans)
                .WithRequired(e => e.Form)
                .HasForeignKey(e => e.IdForm)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InspectionRole>()
                .HasMany(e => e.ITPsSubcontractor)
                .WithRequired(e => e.SubcontractorRole)
                .HasForeignKey(e => e.IdSubcontractorRole)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InspectionRole>()
                .HasMany(e => e.ITPsContractor)
                .WithRequired(e => e.ContractorRole)
                .HasForeignKey(e => e.IdContractorRole)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InspectionRole>()
                .HasMany(e => e.ITPsOwner)
                .WithOptional(e => e.OwnerRole)
                .HasForeignKey(e => e.IdOwnerRole);

            modelBuilder.Entity<InspectionRole>()
                .HasMany(e => e.ITPsThirdPart)
                .WithOptional(e => e.ThirdPartRole)
                .HasForeignKey(e => e.IdThirdPartRole);

            modelBuilder.Entity<InspectionTestPlan>()
                .HasMany(e => e.Requests)
                .WithRequired(e => e.InspectionTestPlan)
                .HasForeignKey(e => e.IdTestPlan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrganizationRole>()
                .HasMany(e => e.Organizations)
                .WithRequired(e => e.OrganizationRole)
                .HasForeignKey(e => e.IdOrganizationRole)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Organization>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Organization)
                .HasForeignKey(e => e.IdOrganization)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Position>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Position)
                .HasForeignKey(e => e.IdPosition)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Requests)
                .WithRequired(e => e.Project)
                .HasForeignKey(e => e.IdProject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PunchPriority>()
                .HasMany(e => e.Punches)
                .WithRequired(e => e.PunchPriority)
                .HasForeignKey(e => e.IdPriority)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Request>()
                .HasMany(e => e.TagsRequests)
                .WithRequired(e => e.Request)
                .HasForeignKey(e => e.IdRequest)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subactivity>()
                .HasOptional(e => e.InspectionTestPlan)
                .WithRequired(e => e.Subactivity);

            modelBuilder.Entity<Subsyst>()
                .HasMany(e => e.Requests)
                .WithRequired(e => e.Subsyst)
                .HasForeignKey(e => e.IdSubsyst)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subsyst>()
                .HasMany(e => e.Tags)
                .WithRequired(e => e.Subsyst)
                .HasForeignKey(e => e.IdSubsyst)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Syst>()
                .HasMany(e => e.Subsysts)
                .WithRequired(e => e.Syst)
                .HasForeignKey(e => e.IdSyst)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tag>()
                .HasMany(e => e.Punches)
                .WithRequired(e => e.Tag)
                .HasForeignKey(e => e.IdTag)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tag>()
                .HasMany(e => e.TagsRequests)
                .WithRequired(e => e.Tag)
                .HasForeignKey(e => e.IdTag)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.Requests)
                .WithRequired(e => e.Unit)
                .HasForeignKey(e => e.IdUnit)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.Tags)
                .WithRequired(e => e.Unit)
                .HasForeignKey(e => e.IdUnit)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.RaisedPunches)
                .WithRequired(e => e.RaisedByEmployee)
                .HasForeignKey(e => e.IdRaisedByEmployee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.IdUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.RaisedRequests)
                .WithRequired(e => e.RaisedByEmployee)
                .HasForeignKey(e => e.IdRaisedByEmployee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.SubcontractorRequests)
                .WithRequired(e => e.ResponsibleSubcontractor)
                .HasForeignKey(e => e.IdResponsibleSubcontractor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.ContractorRequests)
                .WithRequired(e => e.ResponsibleContractor)
                .HasForeignKey(e => e.IdResponsibleContractor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Work>()
                .HasMany(e => e.Activities)
                .WithRequired(e => e.Work)
                .HasForeignKey(e => e.IdWork)
                .WillCascadeOnDelete(false);
        }
    }
}
