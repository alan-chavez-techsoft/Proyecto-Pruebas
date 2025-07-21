using Raptor.ScriptAnalyzerConsola.Models;
using System.Text.RegularExpressions;
using static Raptor.ScriptAnalyzerConsola.Enumeradores;

namespace Raptor.ScriptAnalyzerConsola.Servicios;

public class SqlScriptAnalyzer
{
    //private static readonly Regex CreateTableRegex = new(@"CREATE\s+TABLE\s+(?:\[(?<Schema>\w+)\]\.|\b(?<Schema>\w+)\.)?\[?(?<Name>\w+)\]?", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    //private static readonly Regex AlterTableRegex = new(@"ALTER\s+TABLE\s+(?:\[(?<Schema>\w+)\]\.|\b(?<Schema>\w+)\.)?\[?(?<Name>\w+)\]?", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    //private static readonly Regex DropObjectRegex = new(@"DROP\s+(TABLE|PROCEDURE|FUNCTION)\s+(?:\[(?<Schema>\w+)\]\.|\b(?<Schema>\w+)\.)?\[?(?<Name>\w+)\]?", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    //private static readonly Regex ProcedureRegex = new(@"(ALTER|CREATE)\s+PROCEDURE\s+(?:\[(?<Schema>\w+)\]\.|\b(?<Schema>\w+)\.)?\[?(?<Name>\w+)\]?", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    //private static readonly Regex FunctionRegex = new(@"(ALTER|CREATE)\s+FUNCTION\s+(?:\[(?<Schema>\w+)\]\.|\b(?<Schema>\w+)\.)?\[?(?<Name>\w+)\]?", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    //private static readonly Regex DropConstraintRegex = new(@"ALTER\s+TABLE\s+(?:\[?dbo\]?\.)?\[?(?<TableName>\w+)\]?\s+DROP\s+CONSTRAINT\s+\[?(?<ConstraintName>\w+)\]?", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    //private static readonly Regex RenameTableRegex = new(@"EXECUTE\s+sp_rename\s+N'\[dbo\]\.\[(?<OldName>[\w_]+)\]',\s+N'(?<NewName>[\w_]+)'", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex CreateTableRegex = new(
    @"^\s*CREATE\s+TABLE\s+(?:\[(?<Schema>\w+)\]\.|\b(?<Schema>\w+)\.)?\[?(?<Name>\w+)\]?",
    RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex AlterTableRegex = new(
        @"^\s*ALTER\s+TABLE\s+(?:\[(?<Schema>\w+)\]\.|\b(?<Schema>\w+)\.)?\[?(?<Name>\w+)\]?",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex DropObjectRegex = new(
        @"^\s*DROP\s+(TABLE|PROCEDURE|FUNCTION)\s+(?:\[(?<Schema>\w+)\]\.|\b(?<Schema>\w+)\.)?\[?(?<Name>\w+)\]?",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex ProcedureRegex = new(
        @"^\s*(ALTER|CREATE)\s+PROCEDURE\s+(?:\[(?<Schema>\w+)\]\.|\b(?<Schema>\w+)\.)?\[?(?<Name>\w+)\]?",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex FunctionRegex = new(
        @"^\s*(ALTER|CREATE)\s+FUNCTION\s+(?:\[(?<Schema>\w+)\]\.|\b(?<Schema>\w+)\.)?\[?(?<Name>\w+)\]?",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex DropConstraintRegex = new(
        @"^\s*ALTER\s+TABLE\s+(?:\[?dbo\]?\.)?\[?(?<TableName>\w+)\]?\s+DROP\s+CONSTRAINT\s+\[?(?<ConstraintName>\w+)\]?",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex RenameTableRegex = new(
        @"^\s*EXECUTE\s+sp_rename\s+N'\[dbo\]\.\[(?<OldName>[\w_]+)\]',\s+N'(?<NewName>[\w_]+)'",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public ScriptAnalysisResult Analyze(string sqlContent)
    {
        var result = new ScriptAnalysisResult();
        var lines = sqlContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

        int i = 0;
        while (i < lines.Length)
        {
            string line = lines[i].Trim();

            if (CreateTableRegex.Match(line) is { Success: true } matchCreate)
            {
                var change = new SqlChange
                {
                    ChangeType = ChangeType.Create,
                    ObjectType = ObjectType.Table,
                    ObjectName = matchCreate.Groups[2].Value
                };

                var tableDefinitionLines = new List<string>();
                int openParens = 0, closeParens = 0;

                // Primero busca si hay un paréntesis en la misma línea del CREATE
                if (line.Contains('('))
                    openParens += line.Count(c => c == '(');
                if (line.Contains(')'))
                    closeParens += line.Count(c => c == ')');

                tableDefinitionLines.Add(line);
                i++;

                // Leer hasta que se cierre el bloque de definición de la tabla o hasta un GO
                while (i < lines.Length && openParens > closeParens && !lines[i].Trim().Equals("GO", StringComparison.OrdinalIgnoreCase))
                {
                    string currentLine = lines[i].Trim();
                    openParens += currentLine.Count(c => c == '(');
                    closeParens += currentLine.Count(c => c == ')');
                    tableDefinitionLines.Add(currentLine);
                    i++;
                }

                // Procesar cada línea de la definición de la tabla
                foreach (var detailLine in tableDefinitionLines.Skip(1)) // omitir línea CREATE
                {
                    var trimmed = detailLine.Trim();

                    // Ignorar líneas vacías o que sean solo paréntesis
                    if (string.IsNullOrWhiteSpace(trimmed) || trimmed.All(c => c == '(' || c == ')') || trimmed.Equals(");"))
                        continue;

                    if (trimmed.StartsWith("CONSTRAINT", StringComparison.OrdinalIgnoreCase))
                    {
                        change.ConstraintsAdded.Add(trimmed);
                    }
                    else if (RenameTableRegex.Match(trimmed) is { Success: true } matchRename)
                    {
                        var oldName = matchRename.Groups["OldName"].Value;
                        var newName = matchRename.Groups["NewName"].Value;

                        if (change.ObjectName == oldName)
                        {
                            change.ChangeType = ChangeType.Alter;
                            change.ObjectName = newName;
                        }
                    }
                    else
                    {
                        change.ColumnsAdded.Add(trimmed);
                    }
                }

                result.Changes.Add(change);
            }




            if (DropConstraintRegex.Match(line) is { Success: true } matchDropConstraint)
            {
                var tableName = matchDropConstraint.Groups["TableName"].Value;
                var constraintName = matchDropConstraint.Groups["ConstraintName"].Value;

                var change = new SqlChange
                {
                    ChangeType = ChangeType.Alter,
                    ObjectType = ObjectType.Table,
                    ObjectName = tableName
                };
                change.ConstraintsDropped.Add(constraintName);
                result.Changes.Add(change);
                i++;
            }

            if (AlterTableRegex.Match(line) is { Success: true } matchAlter)
            {
                var tableName = matchAlter.Groups[2].Value;
                var change = new SqlChange { ChangeType = ChangeType.Alter, ObjectType = ObjectType.Table, ObjectName = tableName };
                i++;
                while (i < lines.Length && !lines[i].Trim().Equals("GO", StringComparison.OrdinalIgnoreCase))
                {
                    var detailLine = lines[i].Trim();
                    if (detailLine.StartsWith("ADD CONSTRAINT", StringComparison.OrdinalIgnoreCase))
                        change.ConstraintsAdded.Add(detailLine);
                    else if (detailLine.StartsWith("DROP CONSTRAINT", StringComparison.OrdinalIgnoreCase))
                        change.ConstraintsDropped.Add(detailLine);
                    else if (detailLine.StartsWith("ADD", StringComparison.OrdinalIgnoreCase))
                        change.ColumnsAdded.Add(detailLine);
                    else if (detailLine.StartsWith("DROP COLUMN", StringComparison.OrdinalIgnoreCase))
                        change.ColumnsDropped.Add(detailLine);
                    else if (!string.IsNullOrWhiteSpace(detailLine))
                        change.OtherChanges.Add(detailLine);
                    i++;
                }
                if (change.ColumnsAdded.Count > 0 || change.ColumnsDropped.Count > 0)//|| change.ConstraintsAdded.Count > 0 || change.ConstraintsDropped.Count > 0)
                    result.Changes.Add(change);

            }

            //if (DropObjectRegex.Match(line) is { Success: true } matchDrop)
            //{
            //    var objectType = Enum.Parse<ObjectType>(matchDrop.Groups[1].Value, true);
            //    var change = new SqlChange { ChangeType = ChangeType.Drop, ObjectType = objectType, ObjectName = matchDrop.Groups[2].Value };
            //    result.Changes.Add(change);
            //    i++;
            //}

            if (ProcedureRegex.Match(line) is { Success: true } matchProc)
            {
                var objectName = matchProc.Groups[3].Value;
                var change = new SqlChange
                {
                    ChangeType = Enum.Parse<ChangeType>(matchProc.Groups[1].Value, true),
                    ObjectType = ObjectType.Procedure,
                    ObjectName = objectName
                };
                i++;
                while (i < lines.Length && !lines[i].Trim().Equals("GO", StringComparison.OrdinalIgnoreCase))
                {
                    change.OtherChanges.Add(lines[i].Trim());
                    i++;
                }
                result.Changes.Add(change);
            }

            if (FunctionRegex.Match(line) is { Success: true } matchFunc)
            {
                var objectName = matchFunc.Groups[3].Value;
                var change = new SqlChange
                {
                    ChangeType = Enum.Parse<ChangeType>(matchFunc.Groups[1].Value, true),
                    ObjectType = ObjectType.Function,
                    ObjectName = objectName
                };
                i++;
                while (i < lines.Length && !lines[i].Trim().Equals("GO", StringComparison.OrdinalIgnoreCase))
                {
                    change.OtherChanges.Add(lines[i].Trim());
                    i++;
                }
                result.Changes.Add(change);
            }

            i++;
        }

        return result;
    }
}