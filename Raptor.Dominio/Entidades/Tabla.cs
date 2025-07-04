using System.ComponentModel.DataAnnotations;

namespace Raptor.Dominio.Entidades
{
    public class Tabla(int object_Id, string table_Name, string table_Schema)
    {
        [Key]
        public int Object_Id { get; set; } = object_Id;
        public string Table_Name { get; set; } = table_Name;
        public string Table_Schema { get; set; } = table_Schema;
    }
}
