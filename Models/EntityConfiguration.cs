using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiConsorcio.Models
{
    public class EntityConfiguration<TEntity> where TEntity : Entity
    {
        public void DefaultConfigs(EntityTypeBuilder<TEntity> builder, string tableName)
        {
            builder.ToTable(tableName);
            builder.HasKey(x => x.Id);
        }
    }
}
