namespace Raptor.Api.Dtos
{
    public class ColumnDto
    {
        public string Column_Name { get; set; } = string.Empty;
        public string Data_Type { get; set; } = string.Empty;
        public string Is_Nullable { get; set; } = string.Empty;
        public short Character_Maximum_Length { get; set; }
        public byte Numeric_Precision { get; set; }
        public byte Numeric_Scale { get; set; }
        public string Column_Default { get; set; } = string.Empty;
        public bool EsIdentity { get; set; }
    }
}
