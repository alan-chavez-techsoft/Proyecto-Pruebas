using System.ComponentModel.DataAnnotations;

namespace Raptor.Dominio.Entidades
{
    public class PrimaryKey(int object_Id, int column_Id)
    {
        public int Object_Id { get; set; } = object_Id;
        public int Column_Id { get; set; } = column_Id;
    }
}
