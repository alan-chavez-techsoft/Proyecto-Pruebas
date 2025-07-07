using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Raptor.Api.Infraestructura.Modelos;
using Raptor.Api.Infraestructura.Models;

namespace Raptor.Api.Infraestructura.Mappeos
{
    public class ColumnModelConfiguration : IEntityTypeConfiguration<ColumnModel>
    {
        public void Configure(EntityTypeBuilder<ColumnModel> builder)
        {
            builder.ToTable("columns", schema: "sys").HasNoKey();

            builder.Property(e => e.Object_Id)
            .HasColumnName("object_id");
            builder.Property(e => e.Column_Id)
            .HasColumnName("column_Id");
            builder.Property(e => e.Name)
            .HasColumnName("name");
            builder.Property(e => e.Max_Length)
            .HasColumnName("max_length"); 
            builder.Property(e => e.Precision)
            .HasColumnName("precision"); 
            builder.Property(e => e.Scale)
            .HasColumnName("scale"); 
            builder.Property(e => e.Default_Object_Id)
            .HasColumnName("default_object_id");
            builder.Property(e => e.Is_Identity)
            .HasColumnName("is_identity");
            builder.Property(e => e.User_Type_Id)
            .HasColumnName("user_type_id");
        }
    }
}
