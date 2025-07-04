using System.ComponentModel.DataAnnotations;

namespace Raptor.Dominio.Entidades
{
    public class Columnas(int object_Id, int ordinal_Position, string column_Name, bool is_Nullable, string data_Type,
        short character_Maximum_Length, byte numeric_Precision, byte numeric_Scale, int default_Object_Id, bool is_Identity)
    {
        [Key]
        public int Object_Id { get; set; } = object_Id;
        public int Ordinal_Position { get; set; } = ordinal_Position;
        public string Column_Name { get; set; } = column_Name;
        public bool Is_Nullable { get; set; } = is_Nullable;
        public string Data_Type { get; set; } = data_Type;
        public short Character_Maximum_Length { get; set; } = character_Maximum_Length;
        public byte Numeric_Precision { get; set; } = numeric_Precision;
        public byte Numeric_Scale { get; set; } = numeric_Scale;
        public int Default_Object_Id { get; set; } = default_Object_Id;
        public bool Is_Identity { get; set; } = is_Identity;
    }
}
