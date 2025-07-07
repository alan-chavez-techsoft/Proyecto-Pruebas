namespace Raptor.Api.Infraestructura.Models
{
    public class TriggerModel
    {
        public TriggerModel(int object_Id, int parent_Id, string name, bool is_Ms_Shipped)
        {
            Object_Id = object_Id;
            Parent_Id = parent_Id;
            Name = name;
            Is_Ms_Shipped = is_Ms_Shipped;
        }

        public int Object_Id { get; set; }
        public int Parent_Id { get; set; }
        public string Name { get; set; }
        public bool Is_Ms_Shipped { get; set; }
    }
}
