using Raptor.ScriptAnalyzerConsola.Dtos;
using Raptor.ScriptAnalyzerConsola.Utilities;

namespace Raptor.ScriptAnalyzerConsola.Servicios;

public class ScriptService
{
    public async Task<AnalizarScriptsResponse> AnalizarScript(string rutaArchivos, List<NombreArchivo> archivos)
    {
        if (!Directory.Exists(rutaArchivos))
            throw new ArgumentException($"La ruta '{rutaArchivos}' no existe o no está disponible.");

        var analyzer = new SqlScriptAnalyzer();
        var response = new AnalizarScriptsResponse();

        foreach (var archivo in archivos.OrderBy(x => x.Orden))
        {
            try
            {
                if (!archivo.Nombre.EndsWith(".sql"))
                    archivo.Nombre = archivo.Nombre + ".sql";

                var sqlContent = await FileHelper.ReadFileAsync($"{rutaArchivos}\\{archivo.Nombre}");
                var analisis = analyzer.Analyze(sqlContent);

                var stop = "stop";

                response.Scripts.Add(new InformacionScript
                {
                    Nombre = archivo.Nombre,
                    Orden = archivo.Orden,
                    Objetos = [..
                            analisis.Changes.SelectMany(x =>
                                x.ColumnsAdded.Select(col => new ObjetosAnalizados
                                {
                                    Nombre = x.ObjectName,
                                    Descripcion = $"Columna agregada: {col}"
                                })
                                .Concat(x.ColumnsDropped.Select(col => new ObjetosAnalizados
                                {
                                    Nombre = x.ObjectName,
                                    Descripcion = $"Columna eliminada: {col}"
                                }))
                                .Concat(x.ColumnsModified.Select(col => new ObjetosAnalizados
                                {
                                    Nombre = x.ObjectName,
                                    Descripcion = $"Columna modificada: {col}"
                                }))
                                .Concat(x.ConstraintsAdded.Select(cons => new ObjetosAnalizados
                                {
                                    Nombre = x.ObjectName,
                                    Descripcion = $"Restricción agregada: {cons}"
                                }))
                                .Concat(x.ConstraintsDropped.Select(cons => new ObjetosAnalizados
                                {
                                    Nombre = x.ObjectName,
                                    Descripcion = $"Restricción eliminada: {cons}"
                                }))
                                .Concat(x.OtherChanges.Select(other => new ObjetosAnalizados
                                {
                                    Nombre = x.ObjectName,
                                    Descripcion = $"Otro cambio: {other}"
                                }))
                                .DefaultIfEmpty(new ObjetosAnalizados
                                {
                                    Nombre = x.ObjectName,
                                    Descripcion = $"Tipo de cambio: {x.ChangeType}, Tipo de objeto: {x.ObjectType}"
                                })
                            )
                    ]
                });
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error: El archivo '{ex.FileName}' no existe.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al analizar el archivo '{archivo.Nombre}': {ex.Message}");
            }
        }

        return response;
    }
}

