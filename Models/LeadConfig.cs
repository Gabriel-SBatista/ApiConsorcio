using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiConsorcio.Models
{
    public class LeadConfig : EntityConfiguration<Lead>, IEntityTypeConfiguration<Lead>
    {
        public void Configure(EntityTypeBuilder<Lead> builder)
        {
            DefaultConfigs(builder, tableName: "Leads");

            builder.Property(l => l.Name).HasMaxLength(30).IsRequired();
            builder.Property(l => l.Email).HasMaxLength(45).IsRequired();
            builder.Property(l => l.City).HasMaxLength(30).IsRequired();
            builder.Property(l => l.Source).HasMaxLength(25).IsRequired();
            builder.Property(l => l.Campaign).HasMaxLength(25).IsRequired();
            builder.Property(l => l.Company).HasMaxLength(45).IsRequired();

            builder.HasMany(l => l.Exports).WithOne(e => e.Lead).HasForeignKey("LeadId");
        }
    }
}
