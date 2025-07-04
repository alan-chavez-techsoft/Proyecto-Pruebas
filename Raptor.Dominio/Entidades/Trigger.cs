using System.ComponentModel.DataAnnotations;

namespace Raptor.Dominio.Entidades
{
    public class Trigger(int parent_Id, string trigger_Name, string trigger_Event, string trigger_Definition)
    {
        [Key]
        public int Parent_Id { get; set; } = parent_Id;
        public string Trigger_Name { get; set; } = trigger_Name;
        public string Trigger_Event { get; set; } = trigger_Event;
        public string Trigger_Definition { get; set; } = trigger_Definition;
    }
}
