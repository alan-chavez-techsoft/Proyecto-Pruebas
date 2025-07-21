using Raptor.ScriptAnalyzerConsola.Dtos;
using Raptor.ScriptAnalyzerConsola.Models;
using Raptor.ScriptAnalyzerConsola.Servicios;
using Raptor.ScriptAnalyzerConsola.Utilities;


var rutaArchivos = @"C:\\PSGT";
var lista = new List<NombreArchivo>
{
    new() { Nombre = "01-PharmacySoftBO_TablasNuevas", Orden = 1 },
    //new() { Nombre = "02-PharmacySoftBO_CamposNuevos", Orden = 2 },
    //new() { Nombre = "03-PharmacySoftBO_ModificaCampos", Orden = 3 },
    //new() { Nombre = "04-PharmacySoftBO_Triggers", Orden = 4 },
    //new() { Nombre = "05-PharmacySoftBO_Constraints", Orden = 5 },
    //new() { Nombre = "06-PharmacySoftBO_250612_Actualizar", Orden = 6 },
    //new() { Nombre = "07-PharmacySoftBO_CargaInicial", Orden = 7 },
    //new() { Nombre = "08-PharmacySoftBO_ActualizaEsquemaSyncphony", Orden = 8 },
    //new() { Nombre = "09-PharmacySoftPV_TablasNuevas", Orden = 9 },
    //new() { Nombre = "10-PharmacySoftPV_CamposNuevos", Orden = 10 },
    //new() { Nombre = "11-PharmacySoftPV_ModificaCampos", Orden = 11 },
    //new() { Nombre = "12-PharmacySoftPV_Triggers", Orden = 12 },
    //new() { Nombre = "13-PharmacySoftPV_Constraints", Orden = 13 },
    //new() { Nombre = "14-PharmacySoftPV_250612_Actualizar", Orden = 14 },
    //new() { Nombre = "15-PharmacySoftPV_CargaInicial", Orden = 15 },
    //new() { Nombre = "16-PharmacySoftAL_TablasNuevas", Orden = 16 },
    //new() { Nombre = "17-PharmacySoftAL_CamposNuevos", Orden = 17 },
    //new() { Nombre = "18-PharmacySoftAL_ModificaCampos", Orden = 18 },
    //new() { Nombre = "19-PharmacySoftAL_Constraints", Orden = 19 },
    //new() { Nombre = "20-PharmacySoftAL_Triggers", Orden = 20 },
    //new() { Nombre = "21-PharmacySoftAL_250612_Actualizar", Orden = 21 },
    //new() { Nombre = "22-PharmacySoftAL_CargaInicial", Orden = 22 },
    //new() { Nombre = "20250717-Script de comparación de esquema de PharmacySoftBackOffice", Orden = 2 },
};

try
{
    var service = new ScriptService();
    var response = await service.AnalizarScript(rutaArchivos, lista);


    var analizer = new SqlScriptAnalyzer();
    analizer.Analyze2($"{rutaArchivos}\\01-PharmacySoftBO_TablasNuevas.sql");
    //analizer.Analyze2($"{rutaArchivos}\\20250717-Script de comparación de esquema de PharmacySoftBackOffice.sql");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}


//----------------------------------------------
//----------------------------------------------
void GenerateReport(ScriptAnalysisResult result)
{
    Console.WriteLine("=== SQL SCRIPT CHANGE ANALYSIS REPORT ===\n");

    ReportSimpleSection("TABLES CREATED:", result.TablesCreated);
    ReportSimpleSection("TABLES MODIFIED:", result.TablesModified);
    ReportSimpleSection("STORED PROCEDURES MODIFIED:", result.ProceduresModified);
    ReportSimpleSection("FUNCTIONS MODIFIED:", result.FunctionsModified);

    Console.WriteLine("=== REPORT COMPLETED ===");
}

void ReportSection(string title, IEnumerable<SqlChange> changes)
{
    Console.WriteLine(title);
    foreach (var change in changes)
    {
        Console.WriteLine($"- {change.ObjectName}");
        var details = change.ColumnsAdded.Concat(change.ConstraintsAdded).Concat(change.OtherChanges).ToList();
        foreach (var detail in details.Take(3))
            Console.WriteLine($"  {detail}");
        if (details.Count > 3)
            Console.WriteLine($"  ... and {details.Count - 3} more lines");
        Console.WriteLine();
    }
}

void ReportSimpleSection(string title, IEnumerable<SqlChange> changes)
{
    Console.WriteLine(title);
    foreach (var change in changes)
        Console.WriteLine($"- {change.ChangeType.ToString().ToUpper()} {change.ObjectName}");
    Console.WriteLine();
}
