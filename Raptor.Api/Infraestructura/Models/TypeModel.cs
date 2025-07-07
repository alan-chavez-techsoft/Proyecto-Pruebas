namespace Raptor.Api.Infraestructura.Models
{
    public class TypeModel
    {
        public TypeModel(int user_Type_Id, string name)
        {
            User_Type_Id = user_Type_Id;
            Name = name;
        }

        public int User_Type_Id { get; set; }
        public string Name { get; set; }
    }
}
