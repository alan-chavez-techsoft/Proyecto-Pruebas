namespace Raptor.Api.Dtos
{
    public class ConsultaDto
    {
        public string Table_Schema { get; set; } = string.Empty;
        public string Table_Name { get; set; } = string.Empty;
        public string Table_Type { get; set; } = string.Empty;
        // Column properties
        public string Column_Name { get; set; } = string.Empty;
        public string Data_Type { get; set; } = string.Empty;
        public string Is_Nullable { get; set; } = string.Empty;
        public short Character_Maximum_Length { get; set; }
        public byte Numeric_Precision { get; set; }
        public byte Numeric_Scale { get; set; }
        public string Column_Default { get; set; } = string.Empty;
        public bool EsIdentity { get; set; }
        // Primary Key properties
        public string PK_Column_Name { get; set; } = string.Empty;
        // Foreign Key properties
        public string FK_Name { get; set; } = string.Empty;
        public string FK_Table_Referenced { get; set; } = string.Empty;
        public string FK_Column_Local { get; set; } = string.Empty;
        public string FK_Column_Referenced { get; set; } = string.Empty;
        // Trigger properties
        public string Trigger_Name { get; set; } = string.Empty;
        public string Trigger_Event { get; set; } = string.Empty;
        public int Trigger_Definition { get; set; }
        // Index properties
        public int Index_Id { get; set; }
        public string Index_Name { get; set; } = string.Empty;
        public string Index_Type { get; set; } = string.Empty;
    }
}
