using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpsFlow.Domain.Entities;

namespace OpsFlow.Infrastructure.Configuration
{
    public class IncidentTaskConfiguration : IEntityTypeConfiguration<IncidentTask>
    {
        public void Configure(EntityTypeBuilder<IncidentTask> builder)
        {
            builder.ToTable("IncidentTask");

            // primaryKeySetup
            builder.HasKey(t=> t.Id);
            builder.Property(t => t.Id)
                .ValueGeneratedNever();

            // fieldMapping 
            builder.Property<string>("_incidentId")
                .HasColumnName("IncidentId")
                .IsRequired();

            builder.Property<string>("_title")
                .HasColumnName("Title")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property<string>("_note")
                .HasColumnName("Note")
                .IsRequired()
                .HasMaxLength(250);

            builder.Property<string>("_createdById")
                .HasColumnName("CreatedById")
                .IsRequired();

            builder.Property<string?>("_abortionNote")
            .HasColumnName("AbortionNote");

            builder.Property<string?>("_assigneeId")
                .HasColumnName("AssigneeId");

            builder.Property<string?>("_assignedById")
                .HasColumnName("AssignedById");
            
            builder.Property<string?>("_startedById")
                .HasColumnName("StartedById");
            
            builder.Property<string?>("_finishedById")
                .HasColumnName("FinishedById");

            builder.Property<string?>("_abortedById")
                .HasColumnName("AbortedById");

            builder.Property<string?>("_deletedById")
                .HasColumnName("DeletedById");

            // enumMapping
            builder.Property("_taskState")
                .HasColumnName("TaskState")
                .HasConversion<string>()
                .IsRequired();

            // relationship
            builder.HasOne(typeof(Incident), "_incident")
                .WithMany("_tasks")
                .HasForeignKey("_incidentId")
                .OnDelete(DeleteBehavior.Cascade);

            // navigationFieldAccess
            builder.Navigation("_incident")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            // softDelete
            builder.HasQueryFilter(t => !t.IsDeleted);
        }
    }
}