using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Raptor.Api.Infraestructura.Models;

namespace Raptor.Api.Infraestructura.Configurations
{
    public class TriggerEventConfiguration : IEntityTypeConfiguration<TriggerEventModel>
    {
        public void Configure(EntityTypeBuilder<TriggerEventModel> builder)
        {
            builder.ToTable("trigger_events", schema: "sys").HasNoKey();

            builder.Property(e => e.Object_Id)
                .HasColumnName("object_id");
            builder.Property(e => e.Type_Desc)
                .HasColumnName("type_desc");
        }
    }
}
