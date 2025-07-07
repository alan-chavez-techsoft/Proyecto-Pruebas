namespace Raptor.Api.Infraestructura.Models
{
    public class IndexColumnModel
    {
        public IndexColumnModel(int object_Id, int column_Id, int index_Id)
        {
            Object_Id = object_Id;
            Column_Id = column_Id;
            Index_Id = index_Id;
        }

        public int Object_Id { get; set; }
        public int Column_Id { get; set; }
        public int Index_Id { get; set; }
    }
}
