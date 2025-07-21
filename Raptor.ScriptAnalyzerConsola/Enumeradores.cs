namespace Raptor.ScriptAnalyzerConsola;

public class Enumeradores
{
    public enum ChangeType
    {
        Create,
        Alter,
        Drop,
        Rename
    }

    public enum ObjectType
    {
        Table,
        Procedure,
        Function,
        View,
        Trigger,
        Index,
        Schema
    }
}
