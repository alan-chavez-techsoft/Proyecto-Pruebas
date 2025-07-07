namespace Raptor.Api.Infraestructura.Models
{
    public class TriggerEventModel
    {
        public TriggerEventModel(int object_Id, string type_Desc)
        {
            Object_Id = object_Id;
            Type_Desc = type_Desc;
        }

        public int Object_Id { get; set; }
        public string Type_Desc { get; set; }
    }
}
