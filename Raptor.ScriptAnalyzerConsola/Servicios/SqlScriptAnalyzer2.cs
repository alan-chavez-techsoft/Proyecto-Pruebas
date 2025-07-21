using System.Text.RegularExpressions;

namespace Raptor.ScriptAnalyzerConsola.Servicios
{
    public class SqlScriptAnalyzer2
    {
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
}
