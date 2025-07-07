using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Raptor.Api.Infraestructura.Models;

namespace Raptor.Api.Infraestructura.Configurations
{
    public class KeyConstraintModelConfiguration : IEntityTypeConfiguration<KeyConstraintModel>
    {
        public void Configure(EntityTypeBuilder<KeyConstraintModel> builder)
        {
            builder.ToTable("key_constraints", schema: "sys").HasNoKey();

            builder.Property(e => e.Parent_Object_Id)
                .HasColumnName("parent_Object_Id");
            builder.Property(e => e.Unique_Index_Id)
                .HasColumnName("unique_index_id");
            builder.Property(e => e.Type)
                .HasColumnName("type");
        }
    }
}
