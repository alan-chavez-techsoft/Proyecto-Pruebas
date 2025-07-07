namespace Raptor.Api.Dtos
{
    public class ForeignKeyDto
    {
        public string FK_Name { get; set; } = string.Empty;
        public string FK_Table_Referenced { get; set; } = string.Empty;
        public string FK_Column_Local { get; set; } = string.Empty;
        public string FK_Column_Referenced { get; set; } = string.Empty;
    }
}
