using System.ComponentModel.DataAnnotations;

namespace Raptor.Dominio.Entidades
{
    public class Indice(int object_Id, int column_Id, int index_Id, string index_Name, string index_Type)
    {
        public int Object_Id { get; set; } = object_Id;
        public int Column_Id { get; set; } = column_Id;
        public int Index_Id { get; set; } = index_Id;
        public string Index_Name { get; set; } = index_Name;
        public string Index_Type { get; set; } = index_Type;
    }
}
