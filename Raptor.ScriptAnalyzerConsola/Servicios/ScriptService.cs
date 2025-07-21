using Raptor.ScriptAnalyzerConsola.Dtos;
using Raptor.ScriptAnalyzerConsola.Utilities;
using static Raptor.ScriptAnalyzerConsola.Enumeradores;

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
                var tables = analisis.Changes
                    .Where(x => x.ObjectType == ObjectType.Table)
                    .ToList();
                var procedures = analisis.Changes
                    .Where(x => x.ObjectType == ObjectType.Procedure)
                    .ToList();
                var functions = analisis.Changes
                    .Where(x => x.ObjectType == ObjectType.Function)
                    .ToList();
                var views = analisis.Changes
                    .Where(x => x.ObjectType == ObjectType.View)
                    .ToList();
                var triggers = analisis.Changes
                    .Where(x => x.ObjectType == ObjectType.Trigger)
                    .ToList();
                var indexes = analisis.Changes
                    .Where(x => x.ObjectType == ObjectType.Index)
                    .ToList();
                var schemas = analisis.Changes
                    .Where(x => x.ObjectType == ObjectType.Schema)
                    .ToList();

                var cambios = new Dictionary<ChangeType, string>
                {
                    { ChangeType.Create, "creo" },
                    { ChangeType.Alter, "modifico" },
                    { ChangeType.Drop, "elimino" },
                    { ChangeType.Rename, "reenombro" }
                };


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
                        )
                        .Concat(procedures.Select(t => new ObjetosAnalizados
                        {
                            Nombre = t.ObjectName,
                            Descripcion = $"Se {cambios[t.ChangeType]}: {t.ObjectName}"
                        }))
                        .Concat(functions.Select(t => new ObjetosAnalizados
                        {
                            Nombre = t.ObjectName,
                            Descripcion = $"Se {cambios[t.ChangeType]}: {t.ObjectName}"
                        }))
                        .Concat(tables.Select(t => new ObjetosAnalizados
                        {
                            Nombre = t.ObjectName,
                            Descripcion = $"Se {cambios[t.ChangeType]}: {t.ObjectName}"
                        }))
                        .Concat(views.Select(t => new ObjetosAnalizados
                        {
                            Nombre = t.ObjectName,
                            Descripcion = $"Se {cambios[t.ChangeType]}: {t.ObjectName}"
                        }))
                        .Concat(triggers.Select(t => new ObjetosAnalizados
                        {
                            Nombre = t.ObjectName,
                            Descripcion = $"Se {cambios[t.ChangeType]}: {t.ObjectName}"
                        }))
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

