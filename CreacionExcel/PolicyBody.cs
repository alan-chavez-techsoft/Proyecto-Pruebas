namespace CreacionExcel
{
    public class PolicyBody
    {
        public DateTime FechaDocumento { get; set; }//dd.MM.yyyy
        public string IdSociedad { get; set; } = string.Empty;
        public string ClaseDocumento { get; set; } = string.Empty;
        public DateTime FechaContable { get; set; }//yyyy-MM-dd
        public DateTime Periodo { get; set; }//MM
        public int? TipoCambio { get; } = null;
        public string IdMoneda { get; } = "MXM";
        public int ReferenciaCabecero { get; set; }//Pendiente
        public string TextoCabecero { get; set; } = string.Empty;//Pendiente
        public string LlaveSistema { get; set; } = string.Empty;//Confirmar
        public string PrimerNumeroReferencia { get; set; } = string.Empty;//Confirmar

        //Detalle
        public int NumeroPosicion { get; set; }
        public int ClaveContable { get; set; }
        public int NumeroCuenta { get; set; }
        public decimal Importe { get; set; }
        public string CentroCostosId { get; set; } = string.Empty;
        public string AsignacionId { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;//PENDIENTE
        public int IndicadorIva { get; set; }
        public decimal? ImporteImpuesto { get; set; }
        public string DivisionId { get; set; } = string.Empty;
    }

}
