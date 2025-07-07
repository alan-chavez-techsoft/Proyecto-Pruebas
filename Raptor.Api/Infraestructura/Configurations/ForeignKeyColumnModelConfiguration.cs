using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Raptor.Api.Infraestructura.Models;

namespace Raptor.Api.Infraestructura.Configurations
{
    public class ForeignKeyColumnModelConfiguration : IEntityTypeConfiguration<ForeignKeyModel>
    {
        public void Configure(EntityTypeBuilder<ForeignKeyModel> builder)
        {
            builder.ToTable("foreign_keys", schema: "sys").HasNoKey();

            builder.Property(e => e.Object_Id)
                .HasColumnName("object_id");
            builder.Property(e => e.Name)
                .HasColumnName("name");
            builder.Property(e => e.Parent_Object_Id)
                .HasColumnName("parent_object_id");
        }
    }
}
