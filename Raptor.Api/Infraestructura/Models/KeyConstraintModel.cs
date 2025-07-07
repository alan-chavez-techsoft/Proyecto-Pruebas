namespace Raptor.Api.Infraestructura.Models
{
    public class KeyConstraintModel
    {
        public KeyConstraintModel(int parent_Object_Id, int unique_Index_Id, string type)
        {
            Parent_Object_Id = parent_Object_Id;
            Unique_Index_Id = unique_Index_Id;
            Type = type;
        }

        public int Parent_Object_Id { get; set; }
        public int Unique_Index_Id { get; set; }
        public string Type { get; set; }
    }
}
