using EncriptadorColector;
using ProcesosFundacion.Entidades.Satelite;
using ProcesosFundacion.Tools.Utilerias;

internal class Program
{
    private static void Main(string[] args)
    {
        var accesoSimetricoGlobal = "Do3VJxoVc9QBzMpk6/Vhh7xH0pqd+784Sva9BjNR6YY=";
        var codigoAutenticacionHashGlobal = "m0sfw6fhuU8vhvJoxZ0r6ZWFZmp26kRh97eihPJntfI=";

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
        var accesoSimetricoResponse = "k9i4O8df+ByVAyiD70u7v89O0CAU0+np6aWQUw6E+BCd8FDHQ3Q2FxTyTzYapdWcr1QQDWOH9Nit26bBDLT5CJ5j1lb5iSRIc+U/ZRzWH4w6QslQehkdmLBghtdPGalD";
        var codigoAutenticacionHashResponse = "Vug7ABfRyvOibwFLgc+2wbb+E8ci5wgk+ZZAxQ2H2MxQcyOmTO4ePcs1qQJ/Q2XkXghy2BM4zbxpRaypDSxvkskNXYxCMVadynglYtRhp+8Q4dtK5AYl8GqzCztuZ1T7";
        accesoSimetricoResponse = Encriptador.DescifrarCbcHmac(accesoSimetricoGlobal, codigoAutenticacionHashGlobal, accesoSimetricoResponse);
        codigoAutenticacionHashResponse = Encriptador.DescifrarCbcHmac(accesoSimetricoGlobal, codigoAutenticacionHashGlobal, codigoAutenticacionHashResponse);
        Console.WriteLine("Clave acceso simetrico: " + accesoSimetricoResponse);
        Console.WriteLine("Clave codigo autenticacion hash: " + codigoAutenticacionHashResponse);

        var mensaje = "1.0";
        var mensajeEncriptadoElektra = "9o3LZ+nmv9LP3iM0ZWnbnQ+Hd/W9pgWcHILBnHceE7/1Ip6EBZob/otahfcJBTP/oNm8RTmPK1mdOBPLzihCw==";
        var mensajeEncriptado = Crypto.EncryptAes(accesoSimetricoResponse, codigoAutenticacionHashResponse, mensaje);
        var mensajeDesencriptado = Crypto.DecryptAes(accesoSimetricoResponse, codigoAutenticacionHashResponse, mensajeEncriptado);


        if (mensajeEncriptado != mensajeEncriptadoElektra)
            Console.WriteLine("Error en cifrado");

        if( mensaje != mensajeDesencriptado)
            Console.WriteLine("Error en descifrado");

        Console.WriteLine("Mensaje original: " + mensaje);
        Console.WriteLine("Mensaje encriptado (Elektra): " + mensajeEncriptadoElektra);
        Console.WriteLine("Mensaje encriptado: " + mensajeEncriptado);
        Console.WriteLine("Mensaje desencriptado: " + mensajeDesencriptado);


        var request = CrearRequestCompletoElektra(accesoSimetricoResponse, codigoAutenticacionHashResponse);
        var json = System.Text.Json.JsonSerializer.Serialize(
            request,
            new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase }
        );
        #endregion
    }
    private static CrearPolizaContableRequest CrearRequestCompletoElektra(string accesoSimetrico, string codigoAutenticacionhash)
    {
        // Datos originales para encriptar
        var idSociedad = "MX01";
        var claseDocumento = "SA";
        var codigoMoneda = "MXN01";

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
            ReferenciaCabecero = "REF123456789012",
            TextoCabecero = "Pago de servicios varios",
            LlaveSistema = "LLAVE-SISTEMA-1234567890",
            PrimerNumeroReferencia = "REF-PRIM-12345",
            SegundoNumeroReferencia = "REF-SEG-67890",
            Uuid = "123e4567-e89b-12d3-a456-426614174000",
            InformacionAvisoPagos = "AvisoPagos-1234567890",
            GrupoLedgers = "GL01",
            Usuario = "usuario.demo",
            DetallesPoliza =
            [
                new DetallePolizaDto
                {
                    IdSociedad = "MX01",
                    NumeroPosicionDocumento = "0000000001",
                    ClaveContable = "400000",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionhash, "1234567890"),
                    IndicadorCme = "A",
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionhash, "1000.00"),
                    CodigoCentroCostos = "CC1001",
                    CodigoAsignacion = "ASIG123456789012",
                    Texto = "Detalle de la póliza",
                    NumeroOrden = "ORD123456789",
                    PosicionPresupuestaria = "PP12345678901",
                    CodigoCentroGestor = "CGESTOR123456",
                    CodigoCondicionPago = "COND1",
                    ViaPago = "1",
                    BloqueoPago = "0",
                    IndicadorIva = "IV",
                    ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionhash, "160.00"),
                    CodigoCentroBeneficio = "BENEF12345",
                    ElementoPep = "PEP12345678901234567890",
                    NumeroPersonal = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionhash, "12345678"),
                    AreaFuncional = "AFUNCIONAL",
                    FechaBase = "29.08.2025",
                    CodigoDivision = "DIV1",
                    PrimerNumeroReferencia = "REFNUM1",
                    SegundoNumeroReferencia = "REFNUM2",
                    TercerNumeroReferencia = "REFNUM3",
                    Ramo = "RAMO1",
                    Auxiliar = "AUX1",
                    SubcuentaSegurosAzteca = "SUBSEG1",
                    Grupo = "GRUPO1",
                    UnidadNegocio = "UNEG1",
                    Canal = "CANAL1",
                    Negocio = "NEGOCIO1",
                    NaturalezaCentroCostos = "NATCC1",
                    Subcuenta = "SUBCTA1",
                    Clasificacion = "CLASIF1",
                    IndicadorIsr = "ISR1",
                    IndicadorIetu = "IETU1",
                    SociedadGl = "GL1234",
                    Proyecto = "PROYECTO1",
                    BanderaRetencionesAcreedor = "S",
                    NumeroCuentaBancaria = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionhash, "123456789012345678"),
                    CuentaDivergente = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionhash, "876543210987654321"),
                    DocumentoPresupuestal = "DOC1234567",
                    Fondo = "FONDO1234",
                    ClaseMovimiento = "CM1"
                }
            ],
            ProveedoresUnicaVez =
            [
                new ProveedorUnicaVezDto
                {
                    Tratamiento = "Sr.",
                    Nombre = "Juan Pérez Ramírez",
                    Poblacion = "Ciudad de México",
                    ClavePais = "MEX",
                    CodigoPostal = "01234",
                    Region = "CDMX",
                    CodigoBancario = "BANAMEX12345",
                    ClavePaisBanco = "MEX",
                    CuentaBancaria = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionhash, "123456789012345678"),
                    NumeroIdentificacionFiscal = "RFC123456789012"
                }
            ],
            DetallesRetencion =
            [
                new DetalleRetencionDto
                {
                    PosicionRetencion = "001",
                    TipoRetencion = "01",
                    IndicadorRetencion = "A",
                    BaseImponibleRetencion = "1000.00",
                    ImporteRetencionMonedaDocumento = "160.00"
                }
            ]
        };
    }

    private static CrearPolizaContableRequest CrearRequestCompletoPropio(string accesoSimetrico, string codigoAutenticacionhash)
    {
        // Datos originales para encriptar
        var idSociedad = "MX01";
        var claseDocumento = "SA";
        var codigoMoneda = "MXN01";
        
        // Encriptar los campos requeridos
        var idSociedadEnc = Encriptador.CifrarCbcHmac(accesoSimetrico, codigoAutenticacionhash, idSociedad);
        var claseDocumentoEnc = Encriptador.CifrarCbcHmac(accesoSimetrico, codigoAutenticacionhash, claseDocumento);
        var codigoMonedaEnc = Encriptador.CifrarCbcHmac(accesoSimetrico, codigoAutenticacionhash, codigoMoneda);

        return new CrearPolizaContableRequest
        {
            FechaDocumento = "29.08.2025", // dd.MM.yyyy,
            IdSociedad = idSociedadEnc,
            ClaseDocumento = claseDocumentoEnc,
            FechaContable = "2025-08-29",
            Periodo = "08",
            TipoCambio = "17.50000000",
            CodigoMoneda = codigoMonedaEnc,
            ReferenciaCabecero = "REF123456789012",
            TextoCabecero = "Pago de servicios varios",
            LlaveSistema = "LLAVE-SISTEMA-1234567890",
            PrimerNumeroReferencia = "REF-PRIM-12345",
            SegundoNumeroReferencia = "REF-SEG-67890",
            Uuid = "123e4567-e89b-12d3-a456-426614174000",
            InformacionAvisoPagos = "AvisoPagos-1234567890",
            GrupoLedgers = "GL01",
            Usuario = "usuario.demo",
            DetallesPoliza =
            [
                new DetallePolizaDto
                {
                    IdSociedad = "MX01",
                    NumeroPosicionDocumento = "0000000001",
                    ClaveContable = "400000",
                    NumeroCuenta = Encriptador.CifrarCbcHmac(accesoSimetrico, codigoAutenticacionhash, "1234567890"),
                    IndicadorCme = "A",
                    Importe = Encriptador.CifrarCbcHmac(accesoSimetrico, codigoAutenticacionhash, "1000.00"),
                    CodigoCentroCostos = "CC1001",
                    CodigoAsignacion = "ASIG123456789012",
                    Texto = "Detalle de la póliza",
                    NumeroOrden = "ORD123456789",
                    PosicionPresupuestaria = "PP12345678901",
                    CodigoCentroGestor = "CGESTOR123456",
                    CodigoCondicionPago = "COND1",
                    ViaPago = "1",
                    BloqueoPago = "0",
                    IndicadorIva = "IV",
                    ImporteImpuesto = Encriptador.CifrarCbcHmac(accesoSimetrico, codigoAutenticacionhash, "160.00"),
                    CodigoCentroBeneficio = "BENEF12345",
                    ElementoPep = "PEP12345678901234567890",
                    NumeroPersonal = Encriptador.CifrarCbcHmac(accesoSimetrico, codigoAutenticacionhash, "12345678"),
                    AreaFuncional = "AFUNCIONAL",
                    FechaBase = "29.08.2025",
                    CodigoDivision = "DIV1",
                    PrimerNumeroReferencia = "REFNUM1",
                    SegundoNumeroReferencia = "REFNUM2",
                    TercerNumeroReferencia = "REFNUM3",
                    Ramo = "RAMO1",
                    Auxiliar = "AUX1",
                    SubcuentaSegurosAzteca = "SUBSEG1",
                    Grupo = "GRUPO1",
                    UnidadNegocio = "UNEG1",
                    Canal = "CANAL1",
                    Negocio = "NEGOCIO1",
                    NaturalezaCentroCostos = "NATCC1",
                    Subcuenta = "SUBCTA1",
                    Clasificacion = "CLASIF1",
                    IndicadorIsr = "ISR1",
                    IndicadorIetu = "IETU1",
                    SociedadGl = "GL1234",
                    Proyecto = "PROYECTO1",
                    BanderaRetencionesAcreedor = "S",
                    NumeroCuentaBancaria = Encriptador.CifrarCbcHmac(accesoSimetrico, codigoAutenticacionhash, "123456789012345678"),
                    CuentaDivergente = Encriptador.CifrarCbcHmac(accesoSimetrico, codigoAutenticacionhash, "876543210987654321"),
                    DocumentoPresupuestal = "DOC1234567",
                    Fondo = "FONDO1234",
                    ClaseMovimiento = "CM1"
                }
            ],
            ProveedoresUnicaVez =
            [
                new ProveedorUnicaVezDto
                {
                    Tratamiento = "Sr.",
                    Nombre = "Juan Pérez Ramírez",
                    Poblacion = "Ciudad de México",
                    ClavePais = "MEX",
                    CodigoPostal = "01234",
                    Region = "CDMX",
                    CodigoBancario = "BANAMEX12345",
                    ClavePaisBanco = "MEX",
                    CuentaBancaria = Encriptador.CifrarCbcHmac(accesoSimetrico, codigoAutenticacionhash, "123456789012345678"),
                    NumeroIdentificacionFiscal = "RFC123456789012"
                }
            ],
            DetallesRetencion =
            [
                new DetalleRetencionDto
                {
                    PosicionRetencion = "001",
                    TipoRetencion = "01",
                    IndicadorRetencion = "A",
                    BaseImponibleRetencion = "1000.00",
                    ImporteRetencionMonedaDocumento = "160.00"
                }
            ]
        };
    }
}