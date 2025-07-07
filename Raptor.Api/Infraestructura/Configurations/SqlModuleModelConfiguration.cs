using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Raptor.Api.Infraestructura.Models;

namespace Raptor.Api.Infraestructura.Configurations
{
    public class SqlModuleModelConfiguration : IEntityTypeConfiguration<SqlModuleModel>
    {
        public void Configure(EntityTypeBuilder<SqlModuleModel> builder)
        {
            builder.ToTable("sql_modules", schema: "sys").HasNoKey();

            builder.Property(e => e.Object_Id)
                .HasColumnName("object_id");
            builder.Property(e => e.Definition)
                .HasColumnName("definition");
        }
    }
}
