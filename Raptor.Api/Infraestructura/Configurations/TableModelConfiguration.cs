using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Raptor.Api.Infraestructura.Models;

namespace Raptor.Api.Infraestructura.Configurations
{
    public class TableModelConfiguration : IEntityTypeConfiguration<TableModel>
    {
        public void Configure(EntityTypeBuilder<TableModel> builder)
        {
            builder.ToTable("tables", schema: "sys").HasNoKey();

            builder.Property(e => e.Schema_Id)
                .HasColumnName("schema_id");
            builder.Property(e => e.Object_Id)
                .HasColumnName("object_Id");
            builder.Property(e => e.Name)
                .HasColumnName("name");
            builder.Property(e => e.Is_Ms_Shipped)
                .HasColumnName("is_ms_shipped");
        }
    }
}
