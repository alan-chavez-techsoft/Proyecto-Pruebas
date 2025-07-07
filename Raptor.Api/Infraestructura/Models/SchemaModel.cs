namespace Raptor.Api.Infraestructura.Models
{
    public class SchemaModel
    {
        public SchemaModel(int schema_Id, string name)
        {
            Schema_Id = schema_Id;
            Name = name;
        }

        public int Schema_Id { get; set; }
        public string Name { get; set; }
    }
}
