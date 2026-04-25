using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpsFlow.Domain.Entities;

namespace OpsFlow.Infrastructure.Configuration
{
    public class IncidentHistoryConfiguration : IEntityTypeConfiguration<IncidentHistory>
    {
        public void Configure(EntityTypeBuilder<IncidentHistory> builder)
        {
            builder.ToTable("Histories");

            // primaryKeySetup
            builder.HasKey(h=> h.Id);
            builder.Property(h => h.Id)
                .ValueGeneratedNever();

            // fieldMapping 
            builder.Property<string>("_incidentId")
                .HasColumnName("IncidentId")
                .IsRequired();

            builder.Property<string?>("_taskId")
                .HasColumnName("TaskId");

            builder.Property<string>("_performedById")
                .HasColumnName("PerformedById")
                .IsRequired();

            builder.Property<string>("_note")
                .HasColumnName("Note")
                .IsRequired()
                .HasMaxLength(250);

            builder.Property<DateTime>("_occuredAt")
                .HasColumnName("OccuredAt")
                .IsRequired();

            // enumMapping
            builder.Property("_eventType")
                .HasColumnName("EventType")
                .HasConversion<string>()
                .IsRequired();

            // relationship
            builder.HasOne<Incident>()
                .WithMany()
                .HasForeignKey("_incidentId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<IncidentTask>()
                .WithMany()
                .HasForeignKey("_taskId")
                .OnDelete(DeleteBehavior.NoAction);
            
            // softDelete
            builder.HasQueryFilter(h => !h.IsDeleted);
        }
    }
}