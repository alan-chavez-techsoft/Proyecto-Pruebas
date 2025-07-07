using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Raptor.Api.Infraestructura.Models;

namespace Raptor.Api.Infraestructura.Configurations
{
    public class IndexModelConfiguration : IEntityTypeConfiguration<IndexModel>
    {
        public void Configure(EntityTypeBuilder<IndexModel> builder)
        {
            builder.ToTable("indexes", schema: "sys").HasNoKey();

            builder.Property(e => e.Name)
                .HasColumnName("name");
            builder.Property(e => e.Type_Desc)
                .HasColumnName("type_desc");
            builder.Property(e => e.Object_Id)
                .HasColumnName("object_id");
            builder.Property(e => e.Index_Id)
                .HasColumnName("index_id");

        }
    }
}
