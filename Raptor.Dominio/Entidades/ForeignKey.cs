using System.ComponentModel.DataAnnotations;

namespace Raptor.Dominio.Entidades
{
    public class ForeignKey(int parent_Object_Id, int parent_Column_Id, string fK_Name, string referenced_Table, 
        string referenced_Column, string local_Column)
    {
        [Key]
        public int Parent_Object_Id { get; set; } = parent_Object_Id;
        public int Parent_Column_Id { get; set; } = parent_Column_Id;
        public string FK_Name { get; set; } = fK_Name;
        public string Referenced_Table { get; set; } = referenced_Table;
        public string Referenced_Column { get; set; } = referenced_Column;
        public string Local_Column { get; set; } = local_Column;

    }
}
