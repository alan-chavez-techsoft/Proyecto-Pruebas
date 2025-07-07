namespace Raptor.Api.Dtos
{
    public class ConsultaDto
    {
        public string Table_Schema { get; set; } = string.Empty;
        public string Table_Name { get; set; } = string.Empty;
        public string Table_Type { get; set; } = string.Empty;
        // Column properties
        public List<ColumnDto> Columnas { get; set; } = new List<ColumnDto>();
        // Primary Key properties
        public List<PrimaryKeyDto> PrimaryKeys { get; set; } = new List<PrimaryKeyDto>();
        // Foreign Key properties
        public List<ForeignKeyDto> ForeignKeys { get; set; } = new List<ForeignKeyDto>();
        // Trigger properties
        public List<TriggerDto> Triggers { get; set; } = new List<TriggerDto>();
        // Index properties
        public List<IndexDto> Indices { get; set; } = new List<IndexDto>();
    }
}
