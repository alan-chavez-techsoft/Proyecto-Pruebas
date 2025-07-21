using Raptor.ScriptAnalyzerConsola.Models;
using System.Text.RegularExpressions;
using static Raptor.ScriptAnalyzerConsola.Enumeradores;

namespace Raptor.ScriptAnalyzerConsola.Servicios;

public class SqlScriptAnalyzer
{
    private static readonly Regex CreateTableRegex = new(@"CREATE\s+TABLE\s+(?:\[(?<Schema>\w+)\]\.|\b(?<Schema>\w+)\.)?\[?(?<Name>\w+)\]?", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex AlterTableRegex = new(@"ALTER\s+TABLE\s+(?:\[(?<Schema>\w+)\]\.|\b(?<Schema>\w+)\.)?\[?(?<Name>\w+)\]?", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex DropObjectRegex = new(@"DROP\s+(TABLE|PROCEDURE|FUNCTION)\s+(?:\[(?<Schema>\w+)\]\.|\b(?<Schema>\w+)\.)?\[?(?<Name>\w+)\]?", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex ProcedureRegex = new(@"(ALTER|CREATE)\s+PROCEDURE\s+(?:\[(?<Schema>\w+)\]\.|\b(?<Schema>\w+)\.)?\[?(?<Name>\w+)\]?", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex FunctionRegex = new(@"(ALTER|CREATE)\s+FUNCTION\s+(?:\[(?<Schema>\w+)\]\.|\b(?<Schema>\w+)\.)?\[?(?<Name>\w+)\]?", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex DropConstraintRegex = new(@"ALTER\s+TABLE\s+(?:\[?dbo\]?\.)?\[?(?<TableName>\w+)\]?\s+DROP\s+CONSTRAINT\s+\[?(?<ConstraintName>\w+)\]?", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex RenameTableRegex = new(@"EXECUTE\s+sp_rename\s+N'\[dbo\]\.\[(?<OldName>[\w_]+)\]',\s+N'(?<NewName>[\w_]+)'", RegexOptions.IgnoreCase | RegexOptions.Compiled);


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
                var change = new SqlChange { ChangeType = ChangeType.Create, ObjectType = ObjectType.Table, ObjectName = matchCreate.Groups[1].Value };
                i++;
                while (i < lines.Length && !lines[i].Trim().Equals("GO", StringComparison.OrdinalIgnoreCase))
                {
                    var detailLine = lines[i].Trim();
                    if (detailLine.StartsWith("CONSTRAINT", StringComparison.OrdinalIgnoreCase))
                        change.ConstraintsAdded.Add(detailLine);
                    else if (RenameTableRegex.Match(detailLine) is { Success: true } matchRename)
                    {
                        var oldName = matchRename.Groups["OldName"].Value;
                        var newName = matchRename.Groups["NewName"].Value;

                        if (change.ObjectName == oldName)
                        {
                            change.ChangeType = ChangeType.Alter;
                            change.ObjectName = newName;
                            i++;
                            continue;
                        }


                    }
                    else if (!string.IsNullOrWhiteSpace(detailLine))
                        change.ColumnsAdded.Add(detailLine);
                    i++;
                }
                result.Changes.Add(change);
                continue;
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
                continue;
            }

            if (AlterTableRegex.Match(line) is { Success: true } matchAlter)
            {
                var tableName = matchAlter.Groups[1].Value;
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

                continue;
            }

            //if (DropObjectRegex.Match(line) is { Success: true } matchDrop)
            //{
            //    var objectType = Enum.Parse<ObjectType>(matchDrop.Groups[1].Value, true);
            //    var change = new SqlChange { ChangeType = ChangeType.Drop, ObjectType = objectType, ObjectName = matchDrop.Groups[2].Value };
            //    result.Changes.Add(change);
            //    i++;
            //    continue;
            //}

            if (ProcedureRegex.Match(line) is { Success: true } matchProc)
            {
                var objectName = matchProc.Groups[2].Value;
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
                continue;
            }

            if (FunctionRegex.Match(line) is { Success: true } matchFunc)
            {
                var objectName = matchFunc.Groups[2].Value;
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
                continue;
            }

            i++;
        }

        return result;
    }



    #region Analyze2
    public void Analyze2(string rutaArchivo)
    {
        string sql = File.ReadAllText(rutaArchivo);
        sql = EliminarComentarios(sql);

        Dictionary<string, TablaSql> tablas = new();
        List<string> procedimientosCreados = new();

        // Detectar CREATE TABLE
        foreach (Match match in Regex.Matches(sql, @"\bCREATE\s+TABLE\s+((?:\[\w+\]|\w+)\.)?(?:\[\w+\]|\w+)", RegexOptions.IgnoreCase))
        {
            string nombre = ObtenerNombreCompleto(match.Groups[1].Value, match.Value);
            if (!tablas.ContainsKey(nombre))
                tablas[nombre] = new TablaSql { NombreCompleto = nombre };
            tablas[nombre].FueCreada = true;
        }

        // Detectar DROP TABLE
        foreach (Match match in Regex.Matches(sql, @"\bDROP\s+TABLE\s+((?:\[\w+\]|\w+)\.)?(?:\[\w+\]|\w+)", RegexOptions.IgnoreCase))
        {
            string nombre = ObtenerNombreCompleto(match.Groups[1].Value, match.Value);
            if (!tablas.ContainsKey(nombre))
                tablas[nombre] = new TablaSql { NombreCompleto = nombre };
            tablas[nombre].FueEliminada = true;
        }

        // Detectar ALTER TABLE y analizar columnas
        foreach (Match match in Regex.Matches(sql, @"ALTER\s+TABLE\s+((?:\[\w+\]|\w+)\.)?(?:\[\w+\]|\w+)\s+(.*?)\s*;", RegexOptions.IgnoreCase | RegexOptions.Singleline))
        {
            string nombre = ObtenerNombreCompleto(match.Groups[1].Value, match.Value);
            string instrucciones = match.Groups[2].Value;

            if (!tablas.ContainsKey(nombre))
                tablas[nombre] = new TablaSql { NombreCompleto = nombre };

            var tabla = tablas[nombre];

            foreach (Match add in Regex.Matches(instrucciones, @"ADD\s+(?:COLUMN\s+)?\[*([a-zA-Z_][\w]*)\]*\s+[^\s,]+", RegexOptions.IgnoreCase))
                tabla.ColumnasAgregadas.Add(add.Groups[1].Value);

            foreach (Match drop in Regex.Matches(instrucciones, @"DROP\s+COLUMN\s+\[*([a-zA-Z_][\w]*)\]*", RegexOptions.IgnoreCase))
                tabla.ColumnasEliminadas.Add(drop.Groups[1].Value);

            foreach (Match alter in Regex.Matches(instrucciones, @"ALTER\s+COLUMN\s+\[*([a-zA-Z_][\w]*)\]*", RegexOptions.IgnoreCase))
                tabla.ColumnasModificadas.Add(alter.Groups[1].Value);
        }

        // Detectar CREATE PROCEDURE
        foreach (Match match in Regex.Matches(sql, @"\bCREATE\s+PROCEDURE\s+((?:\[\w+\]|\w+)\.)?(?:\[\w+\]|\w+)", RegexOptions.IgnoreCase))
        {
            string nombre = ObtenerNombreCompleto(match.Groups[1].Value, match.Value);
            procedimientosCreados.Add(nombre);
        }

        // Mostrar resultados
        Console.WriteLine("TABLAS ANALIZADAS:");
        if (tablas.Count == 0) Console.WriteLine("  (Ninguna)");

        foreach (var tabla in tablas.Values)
        {
            Console.WriteLine($"Tabla: {tabla.NombreCompleto}");
            if (tabla.FueCreada) Console.WriteLine("Creada");
            if (tabla.FueEliminada) Console.WriteLine("Eliminada");

        }
    }
    static string ObtenerNombreCompleto(string esquemaCapturado, string texto)
    {
        string esquema = string.IsNullOrWhiteSpace(esquemaCapturado) ? "dbo" : esquemaCapturado.TrimEnd('.').Trim('[', ']');
        string nombre = texto.Substring(texto.LastIndexOf('.') + 1).Trim('[', ']', ' ', ';');
        return $"{esquema}.{nombre}";
    }
    static string EliminarComentarios(string sql)
    {
        sql = Regex.Replace(sql, @"--.*?$", "", RegexOptions.Multiline);
        sql = Regex.Replace(sql, @"/\*.*?\*/", "", RegexOptions.Singleline);
        return sql;
    }
    #endregion
}
class TablaSql
{
    public string NombreCompleto { get; set; } = string.Empty;
    public bool FueCreada { get; set; } = false;
    public bool FueEliminada { get; set; } = false;
    public List<string> ColumnasAgregadas { get; set; } = new();
    public List<string> ColumnasEliminadas { get; set; } = new();
    public List<string> ColumnasModificadas { get; set; } = new();
}