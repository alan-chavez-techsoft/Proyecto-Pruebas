namespace Raptor.Api.Infraestructura.Models
{
    public class IndexModel
    {
        public IndexModel(string? name, string type_Desc, int object_Id, int index_Id)
        {
            Name = name;
            Type_Desc = type_Desc;
            Object_Id = object_Id;
            Index_Id = index_Id;
        }

        public string? Name { get; set; }
        public string Type_Desc { get; set; }
        public int Object_Id { get; set; }
        public int Index_Id { get; set; }
    }
}
