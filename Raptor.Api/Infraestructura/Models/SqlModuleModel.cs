namespace Raptor.Api.Infraestructura.Models
{
    public class SqlModuleModel
    {
        public SqlModuleModel(int object_Id, string definition)
        {
            Object_Id = object_Id;
            Definition = definition;
        }

        public int Object_Id { get; set; }
        public string Definition { get; set; }
    }
}
