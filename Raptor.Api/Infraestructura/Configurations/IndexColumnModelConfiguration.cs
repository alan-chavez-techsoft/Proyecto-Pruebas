using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Raptor.Api.Infraestructura.Models;

namespace Raptor.Api.Infraestructura.Configurations
{
    public class IndexColumnModelConfiguration : IEntityTypeConfiguration<IndexColumnModel>
    {
        public void Configure(EntityTypeBuilder<IndexColumnModel> builder)
        {
            builder.ToTable("index_columns", schema: "sys").HasNoKey();

            builder.Property(e => e.Column_Id)
                .HasColumnName("column_id");
            builder.Property(e => e.Object_Id)
                .HasColumnName("object_id");
            builder.Property(e => e.Index_Id)
                .HasColumnName("index_id");
        }
    }
}
