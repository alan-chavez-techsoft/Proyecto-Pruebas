using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Raptor.Api.Infraestructura.Models;

namespace Raptor.Api.Infraestructura.Configurations
{
    public class SchemaModelConfiguration : IEntityTypeConfiguration<SchemaModel>
    {
        public void Configure(EntityTypeBuilder<SchemaModel> builder)
        {
            builder.ToTable("schemas", schema: "sys").HasNoKey();

            builder.Property(e => e.Schema_Id)
                .HasColumnName("schema_id");
            builder.Property(e => e.Name)
                .HasColumnName("name");
        }
    }
}
