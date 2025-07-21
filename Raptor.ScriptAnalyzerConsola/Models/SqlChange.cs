using static Raptor.ScriptAnalyzerConsola.Enumeradores;

namespace Raptor.ScriptAnalyzerConsola.Models;

public class SqlChange
{
    public ChangeType ChangeType { get; set; }
    public ObjectType ObjectType { get; set; }
    public string ObjectName { get; set; } = string.Empty;

    public List<string> ColumnsAdded { get; set; } = new();
    public List<string> ColumnsDropped { get; set; } = new();
    public List<string> ColumnsModified { get; set; } = new();

    public List<string> ConstraintsAdded { get; set; } = new();
    public List<string> ConstraintsDropped { get; set; } = new();

    public List<string> OtherChanges { get; set; } = new();

    public string ScriptFileName { get; set; } = string.Empty;
    public DateTime DetectedAt { get; set; } = DateTime.UtcNow;
}
