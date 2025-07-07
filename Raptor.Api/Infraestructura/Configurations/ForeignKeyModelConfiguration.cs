using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Raptor.Api.Infraestructura.Models;

namespace Raptor.Api.Infraestructura.Configurations
{
    public class ForeignKeyModelConfiguration : IEntityTypeConfiguration<ForeignKeyColumnModel>
    {
        public void Configure(EntityTypeBuilder<ForeignKeyColumnModel> builder)
        {
            builder.ToTable("foreign_key_columns", schema: "sys").HasNoKey();
            
            builder.Property(e => e.Constraint_Object_Id)
                .HasColumnName("constraint_object_id");
            builder.Property(e => e.Parent_Object_Id)
                .HasColumnName("parent_object_id");
            builder.Property(e => e.Parent_Column_Id)
    .HasColumnName("parent_column_id");
            builder.Property(e => e.Referenced_Object_Id)
    .HasColumnName("referenced_object_id");
            builder.Property(e => e.Referenced_Column_Id)
    .HasColumnName("referenced_column_id");
        }
    }
}
