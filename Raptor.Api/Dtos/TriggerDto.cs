namespace Raptor.Api.Dtos
{
    public class TriggerDto
    {
        public string Trigger_Name { get; set; } = string.Empty;
        public string Trigger_Event { get; set; } = string.Empty;
        public int Trigger_Definition { get; set; }
    }
}
