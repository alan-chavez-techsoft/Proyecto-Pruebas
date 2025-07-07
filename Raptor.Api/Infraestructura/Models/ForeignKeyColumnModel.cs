namespace Raptor.Api.Infraestructura.Models
{
    public class ForeignKeyColumnModel
    {
        public ForeignKeyColumnModel(int constraint_Object_Id, int parent_Object_Id, int parent_Column_Id, 
            int referenced_Object_Id, int referenced_Column_Id)
        {
            Constraint_Object_Id = constraint_Object_Id;
            Parent_Object_Id = parent_Object_Id;
            Parent_Column_Id = parent_Column_Id;
            Referenced_Object_Id = referenced_Object_Id;
            Referenced_Column_Id = referenced_Column_Id;
        }

        public int Constraint_Object_Id { get; set; }
        public int Parent_Object_Id { get; set; }
        public int Parent_Column_Id { get; set; }
        public int Referenced_Object_Id { get; set; }
        public int Referenced_Column_Id { get; set; }
    }
}
