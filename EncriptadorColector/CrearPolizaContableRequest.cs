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
        [StringLength(20)]
        public string SegundoNumeroReferencia { get; set; } = string.Empty;
        [StringLength(40)]
        public string Uuid { get; set; } = string.Empty;
        [StringLength(40)]
        public string InformacionAvisoPagos { get; set; } = string.Empty;
        [StringLength(4)]
        public string GrupoLedgers { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public required List<DetallePolizaDto> DetallesPoliza { get; set; } = [];
        public List<ProveedorUnicaVezDto> ProveedoresUnicaVez { get; set; } = [];
        public List<DetalleRetencionDto> DetallesRetencion { get; set; } = [];
    }

    public class DetallePolizaDto
    {
        [StringLength(4)]
        public string IdSociedad { get; set; } = string.Empty;
        [StringLength(10)]
        public required string NumeroPosicionDocumento { get; set; }
        [StringLength(40)]
        public required string ClaveContable { get; set; }
        [StringLength(10)]
        public required string NumeroCuenta { get; set; } //Encriptado
        [StringLength(1)]
        public string IndicadorCme { get; set; } = string.Empty;
        [StringLength(23)]
        public required string Importe { get; set; } //Encriptado
        [StringLength(40)]
        public string CodigoCentroCostos { get; set; } = string.Empty;
        [StringLength(18)]
        public string CodigoAsignacion { get; set; } = string.Empty;
        [StringLength(50)]
        public string Texto { get; set; } = string.Empty;
        [StringLength(12)]
        public string NumeroOrden { get; set; } = string.Empty;
        [StringLength(14)]
        public string PosicionPresupuestaria { get; set; } = string.Empty;
        [StringLength(16)]
        public string CodigoCentroGestor { get; set; } = string.Empty;
        public string CodigoCondicionPago { get; set; } = string.Empty;
        [StringLength(1)]
        public string ViaPago { get; set; } = string.Empty;
        [StringLength(1)]
        public string BloqueoPago { get; set; } = string.Empty;
        [StringLength(2)]
        public string IndicadorIva { get; set; } = string.Empty;
        [StringLength(23)]
        public string ImporteImpuesto { get; set; } = string.Empty; //Encriptado
        [StringLength(10)]
        public string CodigoCentroBeneficio { get; set; } = string.Empty;
        [StringLength(24)]
        public string ElementoPep { get; set; } = string.Empty;
        [StringLength(8)] //Este length es antes de encriptar
        public string NumeroPersonal { get; set; } = string.Empty; //Encriptado
        [StringLength(40)]
        public string AreaFuncional { get; set; } = string.Empty;
        [StringLength(10)]
        public string FechaBase { get; set; } = string.Empty; //Formato: dd.MM.yyyy
        [StringLength(4)]
        public string CodigoDivision { get; set; } = string.Empty;
        [StringLength(12)]
        public string PrimerNumeroReferencia { get; set; } = string.Empty;
        [StringLength(12)]
        public string SegundoNumeroReferencia { get; set; } = string.Empty;
        [StringLength(12)]
        public string TercerNumeroReferencia { get; set; } = string.Empty;
        [StringLength(20)]
        public string Ramo { get; set; } = string.Empty;
        [StringLength(20)]
        public string Auxiliar { get; set; } = string.Empty;
        [StringLength(20)]
        public string SubcuentaSegurosAzteca { get; set; } = string.Empty;
        [StringLength(20)]
        public string Grupo { get; set; } = string.Empty;
        [StringLength(20)]
        public string UnidadNegocio { get; set; } = string.Empty;
        [StringLength(20)]
        public string Canal { get; set; } = string.Empty;
        [StringLength(20)]
        public string Negocio { get; set; } = string.Empty;
        [StringLength(20)]
        public string NaturalezaCentroCostos { get; set; } = string.Empty;
        [StringLength(20)]
        public string Subcuenta { get; set; } = string.Empty;
        [StringLength(20)]
        public string Clasificacion { get; set; } = string.Empty;
        [StringLength(20)]
        public string IndicadorIsr { get; set; } = string.Empty;
        [StringLength(20)]
        public string IndicadorIetu { get; set; } = string.Empty;
        [StringLength(6)]
        public string SociedadGl { get; set; } = string.Empty;
        [StringLength(20)]
        public string Proyecto { get; set; } = string.Empty;
        public string BanderaRetencionesAcreedor { get; set; } = string.Empty;
        [StringLength(18)] //Este length es antes de encriptar
        public string NumeroCuentaBancaria { get; set; } = string.Empty; //Encriptado
        [StringLength(18)] //Este length es antes de encriptar
        public string CuentaDivergente { get; set; } = string.Empty; //Encriptado
        [StringLength(10)]
        public string DocumentoPresupuestal { get; set; } = string.Empty;
        [StringLength(10)]
        public string Fondo { get; set; } = string.Empty;
        [StringLength(3)]
        public string ClaseMovimiento { get; set; } = string.Empty;
    }

    public class ProveedorUnicaVezDto
    {
        [StringLength(15)]
        public string Tratamiento { get; set; } = string.Empty;
        [StringLength(70)]
        public string Nombre { get; set; } = string.Empty;
        [StringLength(35)]
        public string Poblacion { get; set; } = string.Empty;
        [StringLength(3)]
        public string ClavePais { get; set; } = string.Empty;
        [StringLength(10)]
        public string CodigoPostal { get; set; } = string.Empty;
        [StringLength(3)]
        public string Region { get; set; } = string.Empty;
        [StringLength(15)]
        public string CodigoBancario { get; set; } = string.Empty;
        [StringLength(3)]
        public string ClavePaisBanco { get; set; } = string.Empty;
        [StringLength(18)] //Este length es antes de encriptar
        public string CuentaBancaria { get; set; } = string.Empty; //Encriptado
        [StringLength(16)]
        public string NumeroIdentificacionFiscal { get; set; } = string.Empty;
    }

    public class DetalleRetencionDto
    {
        [StringLength(3)]
        public string PosicionRetencion { get; set; } = string.Empty;
        [StringLength(2)]
        public string TipoRetencion { get; set; } = string.Empty;
        [StringLength(1)]
        public string IndicadorRetencion { get; set; } = string.Empty;
        [StringLength(23)]
        public string BaseImponibleRetencion { get; set; } = string.Empty;
        [StringLength(23)]
        public string ImporteRetencionMonedaDocumento { get; set; } = string.Empty;
    }
}
