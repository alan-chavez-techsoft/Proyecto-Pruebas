namespace Raptor.Api.Infraestructura.Models
{
    public class ForeignKeyModel
    {
        public ForeignKeyModel(int object_Id, int parent_Object_Id, string name)
        {
            Object_Id = object_Id;
            Parent_Object_Id = parent_Object_Id;
            Name = name;
        }

        public int Object_Id { get; set; }
        public int Parent_Object_Id { get; set; }
        public string Name { get; set; }
    }
}
