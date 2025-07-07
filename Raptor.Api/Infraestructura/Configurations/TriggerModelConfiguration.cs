using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Raptor.Api.Infraestructura.Models;

namespace Raptor.Api.Infraestructura.Configurations
{
    public class TriggerModelConfiguration : IEntityTypeConfiguration<TriggerModel>
    {
        public void Configure(EntityTypeBuilder<TriggerModel> builder)
        {
            builder.ToTable("triggers", schema: "sys").HasNoKey();

            builder.Property(e => e.Object_Id)
                .HasColumnName("object_id");
            builder.Property(e => e.Name)
                .HasColumnName("name");
            builder.Property(e => e.Parent_Id)
                .HasColumnName("parent_id");
            builder.Property(e => e.Is_Ms_Shipped)
                .HasColumnName("is_ms_shipped");
        }
    }
}
