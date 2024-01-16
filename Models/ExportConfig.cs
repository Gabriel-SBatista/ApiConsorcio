using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiConsorcio.Models
{
    public class ExportConfig : EntityConfiguration<Export>, IEntityTypeConfiguration<Export>, IEntityConfig
    {
        public void Configure(EntityTypeBuilder<Export> builder)
        {
            DefaultConfigs(builder, tableName: "Exports");

            builder.Property(e => e.ExportedBy).IsRequired();
        }
    }
}
