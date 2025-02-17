using ClosedXML.Excel;

namespace CreacionExcel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lista = ObtenerLista();
            GenerarExcel(lista);
        }

        static List<PolicyBody> ObtenerLista()
        {
            var lista = new List<PolicyBody>();
            for (int i = 0; i < 10; i++)
            {
                lista.Add(new PolicyBody
                {
                    FechaDocumento = DateTime.Now,
                    IdSociedad = "IdSociedad",
                    ClaseDocumento = "ClaseDocumento",
                    FechaContable = DateTime.Now,
                    Periodo = DateTime.Now,
                    ReferenciaCabecero = 1,
                    TextoCabecero = "TextoCabecero",
                    LlaveSistema = "LlaveSistema",
                    PrimerNumeroReferencia = "PrimerNumeroReferencia",
                    NumeroPosicion = 1,
                    ClaveContable = 1,
                    NumeroCuenta = 1,
                    Importe = 1,
                    CentroCostosId = "CentroCostosId",
                    AsignacionId = "AsignacionId",
                    Texto = "Texto",
                    IndicadorIva = 1,
                    ImporteImpuesto = 1,
                    DivisionId = "DivisionId"
                });
            }
            return lista;
        }
        static void GenerarExcel(List<PolicyBody> listadoPoliza)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Hoja 1");
                worksheet.Cell("A1").Value = "FechaDocumento";
                worksheet.Cell("B1").Value = "IdSociedad";
                worksheet.Cell("C1").Value = "ClaseDocumento";
                worksheet.Cell("D1").Value = "FechaContable";
                worksheet.Cell("E1").Value = "Periodo";
                worksheet.Cell("F1").Value = "TipoCambio";
                worksheet.Cell("G1").Value = "CodigoMoneda";
                worksheet.Cell("H1").Value = "ReferenciaCabecero";
                worksheet.Cell("I1").Value = "TextoCabecero";
                worksheet.Cell("J1").Value = "LlaveSistema";
                worksheet.Cell("K1").Value = "PrimerNumeroReferencia";
                worksheet.Cell("L1").Value = "NumeroPosicion";
                worksheet.Cell("M1").Value = "ClaveContable";
                worksheet.Cell("N1").Value = "NumeroCuenta";
                worksheet.Cell("O1").Value = "Importe";
                worksheet.Cell("P1").Value = "CodigoCentroCostos";
                worksheet.Cell("Q1").Value = "CodigoAsignacion";
                worksheet.Cell("R1").Value = "Texto";
                worksheet.Cell("S1").Value = "IndicadorIva";
                worksheet.Cell("T1").Value = "ImporteImpuesto";
                worksheet.Cell("U1").Value = "CodigoDivision";



                int fila = 1;
                foreach (var poliza in listadoPoliza)
                {
                    fila++;
                    worksheet.Cell($"A{fila}").Value = poliza.FechaContable;
                    worksheet.Cell($"B{fila}").Value = poliza.IdSociedad;
                    worksheet.Cell($"C{fila}").Value = poliza.ClaseDocumento;
                    worksheet.Cell($"D{fila}").Value = poliza.FechaContable;
                    worksheet.Cell($"E{fila}").Value = poliza.Periodo;
                    worksheet.Cell($"F{fila}").Value = poliza.TipoCambio;
                    worksheet.Cell($"G{fila}").Value = poliza.IdMoneda;
                    worksheet.Cell($"H{fila}").Value = poliza.ReferenciaCabecero;
                    worksheet.Cell($"I{fila}").Value = poliza.TextoCabecero;
                    worksheet.Cell($"J{fila}").Value = poliza.LlaveSistema;
                    worksheet.Cell($"K{fila}").Value = poliza.PrimerNumeroReferencia;
                    worksheet.Cell($"L{fila}").Value = poliza.NumeroPosicion;
                    worksheet.Cell($"M{fila}").Value = poliza.ClaveContable;
                    worksheet.Cell($"N{fila}").Value = poliza.NumeroCuenta;
                    worksheet.Cell($"O{fila}").Value = poliza.Importe;
                    worksheet.Cell($"P{fila}").Value = poliza.CentroCostosId;
                    worksheet.Cell($"Q{fila}").Value = poliza.AsignacionId;
                    worksheet.Cell($"R{fila}").Value = poliza.Texto;
                    worksheet.Cell($"S{fila}").Value = poliza.IndicadorIva;
                    worksheet.Cell($"T{fila}").Value = poliza.ImporteImpuesto;
                    worksheet.Cell($"U{fila}").Value = poliza.DivisionId;
                }
                workbook.SaveAs("C:\\Excels\\Colector-Contable.xlsx");
            }
        }

    }
}
