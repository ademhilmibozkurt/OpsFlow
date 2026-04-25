using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpsFlow.Infrastructure.Entities;

namespace OpsFlow.Infrastructure.Configuration
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("Tokens");

            // primaryKeySetup
            builder.HasKey(t=> t.Id);
            builder.Property(t => t.Id)
                .ValueGeneratedNever();
        }
    }
}