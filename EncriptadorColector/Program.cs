using EncriptadorColector;
using ProcesosFundacion.Entidades.Satelite;
using ProcesosFundacion.Tools.Utilerias;

internal class Program
{
    private static void Main(string[] args)
    {
        #region Llaves globales
        var accesoSimetricoGlobal = "Do3VJxoVc9QBzMpk6/Vhh7xH0pqd+784Sva9BjNR6YY=";
        var codigoAutenticacionHashGlobal = "m0sfw6fhuU8vhvJoxZ0r6ZWFZmp26kRh97eihPJntfI=";
        #endregion

        #region Pruebas propias
        //var accesoSimetricoEncriptado = "H48BdqTUvEygpaD/6DMJIDOKZvPECZe7LNSHHceBHGoAx6DW3jn2cxPdn6imKyEd96D1Z2kxTzUWLAKbowMCge4DIV3WDWO6iWUKclcofW2h1r0QGzHx5oQJMB2Oxv8W";
        //var codigoAutenticacionhashEncriptado = "BYUbWJGP2yclbCoNwLWAse9VjWWKXLhpjTiFekVWFWw5SNRekflZBNPv1r9N5I2yZAZrep9oh77ZvCLnkAxtVNlMI9VyydRiIcbm4ZAFnikLHYgdqLJgZz/R6O1rfwl/";

        //var accesoSimetrico = Encriptador.DescifrarCbcHmac(accesoSimetricoGlobal, codigoAutenticacionHashGlobal, accesoSimetricoEncriptado);
        //var codigoAutenticacionHash = Encriptador.DescifrarCbcHmac(accesoSimetricoGlobal, codigoAutenticacionHashGlobal, codigoAutenticacionhashEncriptado);
        //Console.WriteLine("Clave acceso simetrico: " + accesoSimetrico);
        //Console.WriteLine("Clave codigo autenticacion hash: " + codigoAutenticacionHash);

        //if (accesoSimetricoEncriptado != Encriptador.CifrarCbcHmac(accesoSimetricoGlobal, codigoAutenticacionHashGlobal, accesoSimetrico))
        //    Console.WriteLine("Error en cifrado/descifrado de clave acceso simetrico");

        //Console.WriteLine("--------------------------------------------------");

        //var encriptador = new Encriptador2();
        //var accesoSimetrico = encriptador.DescifradoCbc(accesoSimetricoGlobal, codigoAutenticacionhashGlobal, accesoSimetricoEncriptado);
        //var codigoAutenticacionHash = encriptador.DescifradoCbc(accesoSimetricoGlobal, codigoAutenticacionHashGlobal, codigoAutenticacionhashEncriptado);
        //Console.WriteLine("Clave acceso simetrico: " + accesoSimetrico);
        //Console.WriteLine("Clave codigo autenticacion hash: " + codigoAutenticacionHash);

        //if (accesoSimetricoEncriptado != encriptador.CifradoCbc(accesoSimetricoGlobal, codigoAutenticacionHashGlobal, accesoSimetrico))
        //    Console.WriteLine("Error en cifrado/descifrado de clave acceso simetrico");

        //Console.WriteLine("--------------------------------------------------");

        //var request = CrearRequestCompleto(accesoSimetrico, codigoAutenticacionHash);
        //var json = System.Text.Json.JsonSerializer.Serialize(
        //    request,
        //    new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase }
        //);

        //Console.WriteLine("--------------------------------------------------");
        //var prueba = "Prueba de encriptacion";
        //Console.WriteLine(Encriptador.CifrarCbcHmac(accesoSimetrico, codigoAutenticacionHash, prueba));
        //Console.WriteLine(Encriptador.DescifrarCbcHmac(accesoSimetrico, codigoAutenticacionHash, Encriptador.CifrarCbcHmac(accesoSimetrico, codigoAutenticacionHash, prueba)));
        #endregion
        #region Pruebas Elektra
        var accesoSimetricoResponse = "82kIULqVdkc0ixH6Hesf6I+5DvFC1oGyV3DuYtzoQr/9yTb5tjih4cK82MEQL9ibDj6yIvtC6K7cSLlb03lMlj1fZWKKC35CPty/iQ0ZMrrCqObLVtAmplS+2WFSBTvR";
        var codigoAutenticacionHashResponse = "2oXeVLz8xksiKJBd3ogHPkagi+mf9CLGLXpHiYcSO6rZ5kkz5IkxJUN5+gPc2a6lMTz1ISHbe/uMoSaqCY++5cvkbwHmcLXZ9BSHiM+1QcEFvxSW4L+5vQrXELsudHPY";
        accesoSimetricoResponse = Encriptador.DescifrarCbcHmac(accesoSimetricoGlobal, codigoAutenticacionHashGlobal, accesoSimetricoResponse);
        codigoAutenticacionHashResponse = Encriptador.DescifrarCbcHmac(accesoSimetricoGlobal, codigoAutenticacionHashGlobal, codigoAutenticacionHashResponse);

        #region Consola
        //Console.WriteLine("Clave acceso simetrico: " + accesoSimetricoResponse);
        //Console.WriteLine("Clave codigo autenticacion hash: " + codigoAutenticacionHashResponse);

        //var mensaje = "1.0";
        //var mensajeEncriptado = Crypto.EncryptAes(accesoSimetricoResponse, codigoAutenticacionHashResponse, mensaje);
        //var mensajeDesencriptado = Crypto.DecryptAes(accesoSimetricoResponse, codigoAutenticacionHashResponse, mensajeEncriptado);



        //if( mensaje != mensajeDesencriptado)
        //    Console.WriteLine("Error en descifrado");

        //Console.WriteLine("Mensaje original: " + mensaje);
        //Console.WriteLine("Mensaje encriptado: " + mensajeEncriptado);
        //Console.WriteLine("Mensaje desencriptado: " + mensajeDesencriptado);
        #endregion

        var request = CrearRequestCompletoElektra2(accesoSimetricoResponse, codigoAutenticacionHashResponse);
        var json = System.Text.Json.JsonSerializer.Serialize(
            request,
            new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase }
        );
        var jsonBytes = System.Text.Encoding.UTF8.GetBytes(json);
        var jsonEncriptado = Crypto.EncryptAes(accesoSimetricoResponse, codigoAutenticacionHashResponse, json);
        var jsonEncriptadoBase64 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(jsonEncriptado));
        #endregion
    }
    private static CrearPolizaContableRequest CrearRequestCompletoElektra2(string accesoSimetrico, string codigoAutenticacionhash)
    {
        // Datos originales para encriptar
        var idSociedad = "500";
        var claseDocumento = "KR";
        var codigoMoneda = "MXN";

        // Encriptar los campos requeridos
        var idSociedadEnc = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionhash, idSociedad);
        var claseDocumentoEnc = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionhash, claseDocumento);
        var codigoMonedaEnc = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionhash, codigoMoneda);

        return new CrearPolizaContableRequest
        {
            FechaDocumento = "29.08.2025", // dd.MM.yyyy,
            IdSociedad = idSociedadEnc,
            ClaseDocumento = claseDocumentoEnc,
            FechaContable = "2025-08-29",
            Periodo = "08",
            TipoCambio = "17.50000000",
            CodigoMoneda = codigoMonedaEnc,
            ReferenciaCabecero = "123456789",
            TextoCabecero = "Pago de servicios varios",
            LlaveSistema = "1234567890",
            PrimerNumeroReferencia = "1234567890",
            SegundoNumeroReferencia = "0987654321",
            Uuid = "123e4567-e89b-12d3-a456-426614174000",//Uuid
            InformacionAvisoPagos = "AvisoPagos123",
            GrupoLedgers = "A1",
            Usuario = "usuarioDemo",
            DetallesPoliza =
            [
                new DetallePolizaDto
                {
                    IdSociedad = "0750",
                    NumeroPosicionDocumento = "0000000001",
                    ClaveContable = "40",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionhash, "1234567890"),
                    IndicadorCme = "A",
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionhash, "1000.00"),
                    CodigoCentroCostos = "1020300750",
                    CodigoAsignacion = "408090100",
                    Texto = "Detalle de la póliza",
                    NumeroOrden = "1010203040",
                    PosicionPresupuestaria = "802010",
                    CodigoCentroGestor = "GTEL",
                    CodigoCondicionPago = "PO15",
                    ViaPago = "Z",
                    BloqueoPago = "X",
                    IndicadorIva = "AL",
                    ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionhash, "160.00"),
                    CodigoCentroBeneficio = "102030",
                    ElementoPep = "104050909090",
                    NumeroPersonal = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionhash, "145873"),
                    AreaFuncional = "40",
                    FechaBase = "29.08.2025",
                    CodigoDivision = "EF",
                    PrimerNumeroReferencia = "AERECALLSPS",
                    SegundoNumeroReferencia = "AR1234FAC12",
                    TercerNumeroReferencia = "AW9024NOTA1410 ",
                    Ramo = "ZMETA",
                    Auxiliar = "20",
                    SubcuentaSegurosAzteca = "105023054",
                    Grupo = "DAT",
                    UnidadNegocio = "ZCOSTO",
                    Canal = "ZDISTRIBUCION",
                    Negocio = "ZGTEL",
                    NaturalezaCentroCostos = "ZCOSTO",
                    Subcuenta = "105678",
                    Clasificacion = "AR",
                    IndicadorIsr = "A1",
                    IndicadorIetu = "A21",
                    SociedadGl = "1045",
                    Proyecto = "PAS",
                    BanderaRetencionesAcreedor = "S",
                    NumeroCuentaBancaria = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionhash, "1234567890"),//Checar
                    DocumentoPresupuestal = "1023564718",
                    Fondo = "2040",
                    ClaseMovimiento = "CLS"
                }
            ],
            //ProveedoresUnicaVez =
            //[
            //    new ProveedorUnicaVezDto
            //    {
            //        Tratamiento = "Sr.",
            //        Nombre = "Juan Pérez Ramírez",
            //        Poblacion = "Ciudad de México",
            //        ClavePais = "MX",
            //        CodigoPostal = "01234",
            //        Region = "CDMX",
            //        CodigoBancario = "123",
            //        ClavePaisBanco = "MX",
            //        CuentaBancaria = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionhash, "123456789012345678"),
            //        NumeroIdentificacionFiscal = "RFC123456789012"
            //    }
            //],
            DetallesRetencion =
            [
                new DetalleRetencionDto
                {
                    PosicionRetencion = "001",
                    TipoRetencion = "AT",
                    IndicadorRetencion = "09",
                    BaseImponibleRetencion = "10.1",
                    ImporteRetencionMonedaDocumento = "160.00"
                }
            ]
        };
    }

}