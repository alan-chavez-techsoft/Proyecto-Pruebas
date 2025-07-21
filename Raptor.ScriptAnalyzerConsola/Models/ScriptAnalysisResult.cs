using static Raptor.ScriptAnalyzerConsola.Enumeradores;

namespace Raptor.ScriptAnalyzerConsola.Models;

public class ScriptAnalysisResult
{
    public List<SqlChange> Changes { get; set; } = new();

    public IEnumerable<SqlChange> TablesCreated =>
        Changes.Where(c => c.ObjectType == ObjectType.Table && c.ChangeType == ChangeType.Create);

    public IEnumerable<SqlChange> TablesModified =>
        Changes.Where(c => c.ObjectType == ObjectType.Table && c.ChangeType == ChangeType.Alter);

    public IEnumerable<SqlChange> ProceduresModified =>
        Changes.Where(c => c.ObjectType == ObjectType.Procedure &&
                          (c.ChangeType == ChangeType.Alter || c.ChangeType == ChangeType.Create));

    public IEnumerable<SqlChange> FunctionsModified =>
        Changes.Where(c => c.ObjectType == ObjectType.Function &&
                          (c.ChangeType == ChangeType.Alter || c.ChangeType == ChangeType.Create));

    public IEnumerable<SqlChange> OtherChanges =>
        Changes.Except(TablesCreated)
               .Except(TablesModified)
               .Except(ProceduresModified)
               .Except(FunctionsModified);
}
