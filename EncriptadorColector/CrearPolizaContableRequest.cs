using System.ComponentModel.DataAnnotations;

namespace EncriptadorColector
{
    public class CrearPolizaContableRequest
    {
        [StringLength(10)]
        public required string FechaDocumento { get; set; } //Formato: dd.MM.yyyy
        [StringLength(4)]
        public required string IdSociedad { get; set; } //Encriptado
        [StringLength(2)]
        public required string ClaseDocumento { get; set; } //Encriptado
        [StringLength(10)]
        public required string FechaContable { get; set; } //Formato: yyyy-MM-dd confirmar si es mes y luego dia
        [StringLength(2)]
        public string Periodo { get; set; } = string.Empty;
        [StringLength(12)]
        public string TipoCambio { get; set; } = string.Empty;
        [StringLength(5)]
        public required string CodigoMoneda { get; set; } //Encriptado
        [StringLength(16)]
        public string ReferenciaCabecero { get; set; } = string.Empty;
        [StringLength(25)]
        public string TextoCabecero { get; set; } = string.Empty;
        [StringLength(30)]
        public string LlaveSistema { get; set; } = string.Empty;
        [StringLength(20)]
        public string PrimerNumeroReferencia { get; set; } = string.Empty;
        public required List<DetallePolizaDto> DetallesPoliza { get; set; } = [];
    }

    public class DetallePolizaDto
    {
        [StringLength(10)]
        public required string NumeroPosicionDocumento { get; set; }
        [StringLength(40)]
        public required string ClaveContable { get; set; }
        [StringLength(10)]
        public required string NumeroCuenta { get; set; } //Encriptado
        [StringLength(23)]
        public required string Importe { get; set; } //Encriptado
        [StringLength(40)]
        public string CodigoCentroCostos { get; set; } = string.Empty;
        [StringLength(18)]
        public string CodigoAsignacion { get; set; } = string.Empty;
        [StringLength(50)]
        public string Texto { get; set; } = string.Empty;
        [StringLength(2)]
        public string IndicadorIva { get; set; } = string.Empty;
        [StringLength(23)]
        public string ImporteImpuesto { get; set; } = string.Empty; //Encriptado
        [StringLength(4)]
        public string CodigoDivision { get; set; } = string.Empty;
        [StringLength(12)]
        public string SegundoNumeroReferencia { get; set; } = string.Empty;
    }
}
