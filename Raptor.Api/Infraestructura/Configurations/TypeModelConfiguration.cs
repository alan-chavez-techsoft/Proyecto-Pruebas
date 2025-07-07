using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Raptor.Api.Infraestructura.Models;

namespace Raptor.Api.Infraestructura.Configurations
{
    public class TypeModelConfiguration : IEntityTypeConfiguration<TypeModel>
    {
        public void Configure(EntityTypeBuilder<TypeModel> builder)
        {
            builder.ToTable("types", schema: "sys").HasNoKey();

            builder.Property(e => e.User_Type_Id)
            .HasColumnName("user_type_id");
            builder.Property(e => e.Name)
            .HasColumnName("name");
        }
    }
}
