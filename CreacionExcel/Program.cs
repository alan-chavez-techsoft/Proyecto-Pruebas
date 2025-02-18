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
            for(int i = 0; i < 10; i++)
            {
                lista.Add(new PolicyBody
                {
                    FechaDocumento = DateTime.Now,
                    IdSociedad = "SOC" + i,
                    ClaseDocumento = "CL" + i,
                    FechaContable = DateTime.Now,
                    Periodo = DateTime.Now,
                    ReferenciaCabecero = i,
                    TextoCabecero = "Texto Cabecero " + i,
                    LlaveSistema = i,
                    PrimerNumeroReferencia = i,
                    NumeroPosicion = i,
                    ClaveContable = i,
                    NumeroCuenta = i,
                    ImporteDetalle = 100.50m + i,
                    CentroCostosId = "CC" + i,
                    AsignacionId = "AS" + i,
                    Texto = "Texto " + i,
                    IndicadorIva = "IVA" + i,
                    ImporteImpuesto = 10.50m + i,
                    DivisionId = "DIV" + i,
                    TipoPoliza = "TP" + i,
                    ImporteCabecero = 200.75m + i,
                    ConceptoCabecero = "Concepto Cabecero " + i,
                    ConceptoDetalle = "Concepto Detalle " + i,
                    Renglon = i,
                    CuentaContableId = i,
                    Cargo = 50.25m + i,
                    Abono = 25.75m + i,
                    NombreCuentaContable = "Cuenta Contable " + i
                });
            }
            return lista;
        }
        static void GenerarExcel(List<PolicyBody> listadoPoliza)
        {
            #region Generacion Primer excel
            var woorbook1 = new XLWorkbook();
            var worksheet1 = woorbook1.Worksheets.Add("Polizas");
            worksheet1.Cell("A1").Value = "folioPoliza";
            worksheet1.Cell("B1").Value = "tipo";
            worksheet1.Cell("C1").Value = "fechaPoliza";
            worksheet1.Cell("D1").Value = "importePoliza";
            worksheet1.Cell("E1").Value = "concepto";
            worksheet1.Cell("F1").Value = "renglon";
            worksheet1.Cell("G1").Value = "codigoCuentaContable";
            worksheet1.Cell("H1").Value = "concepto2";
            worksheet1.Cell("I1").Value = "cargo";
            worksheet1.Cell("J1").Value = "abono";
            worksheet1.Cell("K1").Value = "Nombre Cuenta Contable";
            #endregion

            #region Generacion Segundo Excel
            var workbook2 = new XLWorkbook();
            var worksheet2 = workbook2.Worksheets.Add("Polizas");
            worksheet2.Cell("A1").Value = "fechaDocumento";
            worksheet2.Cell("B1").Value = "ddSociedad";
            worksheet2.Cell("C1").Value = "claseDocumento";
            worksheet2.Cell("D1").Value = "fechaContable";
            worksheet2.Cell("E1").Value = "periodo";
            worksheet2.Cell("F1").Value = "tipoCambio";
            worksheet2.Cell("G1").Value = "codigoMoneda";
            worksheet2.Cell("H1").Value = "referenciaCabecero";
            worksheet2.Cell("I1").Value = "textoCabecero";
            worksheet2.Cell("J1").Value = "llaveSistema";
            worksheet2.Cell("K1").Value = "primerNumeroReferencia";
            worksheet2.Cell("L1").Value = "numeroPosicion";
            worksheet2.Cell("M1").Value = "claveContable";
            worksheet2.Cell("N1").Value = "numeroCuenta";
            worksheet2.Cell("O1").Value = "importe";
            worksheet2.Cell("P1").Value = "codigoCentroCostos";
            worksheet2.Cell("Q1").Value = "codigoAsignacion";
            worksheet2.Cell("R1").Value = "texto";
            worksheet2.Cell("S1").Value = "indicadorIva";
            worksheet2.Cell("T1").Value = "importeImpuesto";
            worksheet2.Cell("U1").Value = "codigoDivision";
            #endregion

            int fila = 1;
            foreach (var poliza in listadoPoliza)
            {
                fila++;
                #region Datos Primer Excel
                worksheet1.Cell($"A{fila}").Value = poliza.LlaveSistema;
                worksheet1.Cell($"B{fila}").Value = poliza.TipoPoliza;
                worksheet1.Cell($"C{fila}").Value = poliza.FechaContable;
                worksheet1.Cell($"D{fila}").Value = poliza.ImporteCabecero;
                worksheet1.Cell($"E{fila}").Value = poliza.ConceptoCabecero;
                worksheet1.Cell($"F{fila}").Value = poliza.Renglon;
                worksheet1.Cell($"G{fila}").Value = poliza.CuentaContableId;
                worksheet1.Cell($"H{fila}").Value = poliza.ConceptoDetalle;
                worksheet1.Cell($"I{fila}").Value = poliza.Cargo;
                worksheet1.Cell($"J{fila}").Value = poliza.Abono;
                worksheet1.Cell($"K{fila}").Value = poliza.NombreCuentaContable;
                #endregion

                #region Datos Segundo Excel
                worksheet2.Cell($"A{fila}").Value = poliza.FechaContable;
                worksheet2.Cell($"B{fila}").Value = poliza.IdSociedad;
                worksheet2.Cell($"C{fila}").Value = poliza.ClaseDocumento;
                worksheet2.Cell($"D{fila}").Value = poliza.FechaContable;
                worksheet2.Cell($"E{fila}").Value = poliza.Periodo;
                worksheet2.Cell($"F{fila}").Value = poliza.TipoCambio;
                worksheet2.Cell($"G{fila}").Value = poliza.IdMoneda;
                worksheet2.Cell($"H{fila}").Value = poliza.ReferenciaCabecero;
                worksheet2.Cell($"I{fila}").Value = poliza.TextoCabecero;
                worksheet2.Cell($"J{fila}").Value = poliza.LlaveSistema;
                worksheet2.Cell($"K{fila}").Value = poliza.PrimerNumeroReferencia;
                worksheet2.Cell($"L{fila}").Value = poliza.NumeroPosicion;
                worksheet2.Cell($"M{fila}").Value = poliza.ClaveContable;
                worksheet2.Cell($"N{fila}").Value = poliza.NumeroCuenta;
                worksheet2.Cell($"O{fila}").Value = poliza.ImporteDetalle;
                worksheet2.Cell($"P{fila}").Value = poliza.CentroCostosId;
                worksheet2.Cell($"Q{fila}").Value = poliza.AsignacionId;
                worksheet2.Cell($"R{fila}").Value = poliza.Texto;
                worksheet2.Cell($"S{fila}").Value = poliza.IndicadorIva;
                worksheet2.Cell($"T{fila}").Value = poliza.ImporteImpuesto;
                worksheet2.Cell($"U{fila}").Value = poliza.DivisionId;
                #endregion
            }
            woorbook1.SaveAs($"C:\\ServicioColectorContable\\PSListadoPolizas{DateTime.Now.ToString("yyyyMMdd")}.xlsx");
            workbook2.SaveAs($"C:\\ServicioColectorContable\\PSResumenPolizas{DateTime.Now.ToString("yyyyMMdd")}.xlsx");
        }
    }
}
