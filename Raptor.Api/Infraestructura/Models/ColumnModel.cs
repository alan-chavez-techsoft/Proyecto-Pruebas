using Raptor.Api.Infraestructura.Models;

namespace Raptor.Api.Infraestructura.Modelos
{
    public class ColumnModel
    {
        public ColumnModel(int object_Id, int column_Id, string name, bool is_Nullable, short max_Length, 
            byte precision, byte scale, int default_Object_Id, bool is_Identity, int user_Type_Id)
        {
            Object_Id = object_Id;
            Column_Id = column_Id;
            Name = name;
            Is_Nullable = is_Nullable;
            Max_Length = max_Length;
            Precision = precision;
            Scale = scale;
            Default_Object_Id = default_Object_Id;
            Is_Identity = is_Identity;
            User_Type_Id = user_Type_Id;
        }

        public int Object_Id { get; set; }
        public int Column_Id { get; set; }
        public string Name { get; set; }
        public bool Is_Nullable { get; set; }
        public short Max_Length { get; set; }
        public byte Precision { get; set; }
        public byte Scale { get; set; }
        public int Default_Object_Id { get; set; }
        public bool Is_Identity { get; set; }
        public int User_Type_Id { get; set; }
    }
}
