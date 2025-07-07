namespace Raptor.Api.Infraestructura.Models
{
    public class TableModel
    {
        public TableModel(int object_Id, string name, int schema_Id, bool is_Ms_Shipped)
        {
            Object_Id = object_Id;
            Name = name;
            Schema_Id = schema_Id;
            Is_Ms_Shipped = is_Ms_Shipped;
        }

        public int Object_Id { get; set; }
        public string Name { get; set; }
        public int Schema_Id { get; set; }
        public bool Is_Ms_Shipped { get; set; }
    }
}
