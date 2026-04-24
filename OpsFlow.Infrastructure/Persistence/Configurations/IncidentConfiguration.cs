using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpsFlow.Domain.Entities;

namespace OpsFlow.Infrastructure.Configuration
{
    public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
    {
        public void Configure(EntityTypeBuilder<Incident> builder)
        {
            builder.ToTable("Incidents");

            // primaryKeySetup
            builder.HasKey(i=> i.Id);
            builder.Property(i => i.Id)
                .ValueGeneratedNever();

            // fieldMapping 
            builder.Property<string>("_title")
                .HasColumnName("Title")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property<string>("_description")
                .HasColumnName("Description")
                .IsRequired()
                .HasMaxLength(250);

            builder.Property<string?>("_abortionNote")
            .HasColumnName("AbortionNote");

            builder.Property<string?>("_invastigateById")
                .HasColumnName("InvestigatedById");

            builder.Property<string?>("_closedById")
                .HasColumnName("ClosedById");

            builder.Property<string?>("_abortedById")
                .HasColumnName("AbortedById");

            builder.Property<string?>("_deletedById")
                .HasColumnName("DeletedById");

            builder.Property<string?>("_settedById")
                .HasColumnName("SettedById");

            // enumMapping
            builder.Property("_priority")
                .HasColumnName("Priority")
                .HasConversion<string>()
                .IsRequired();
            
            builder.Property("_state")
                .HasColumnName("State")
                .HasConversion<string>()
                .IsRequired();

            // relationship
            builder.HasMany(typeof(IncidentTask), "_tasks")
                .WithOne("_incident")
                .HasForeignKey("_incidentId")
                .OnDelete(DeleteBehavior.Cascade);

            // navigationAccessMode
            builder.Metadata
                .FindNavigation(nameof(Incident.Tasks))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            // softDelete
            builder.HasQueryFilter(i => !i.IsDeleted);
        }
    }
}