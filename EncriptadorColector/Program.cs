using EncriptadorColector;
using Newtonsoft.Json;
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
        #region Pruebas
        var key = new Key
        {
            AesKeyBase64AccesoSimetrico = "BuTYiF7ohNTGYZNTGFseASIJCbdwBOeFTL6hC2mH+i17IE6nu4uBKuSVMK/gJe1gEbLpkXWDeFbgOmg2ir9+HOdqSBhCo75v8OJ+g+2CiMvKgMnUNkF/nkAIfclyPPDS",
            HmacKeyBase64CodigoAutentificacionHash = "giDL9yV9sHRdvLjUUx+ES4VPPRpwhWVJ3U3BdxuiCDchFAK0cPdJwNTPIcUXQXd9UdeDNXxSoGzKUEw236u5ZbwE2ikpksPesSkhIw74pC8lld/6Sk537KElHZdSsz8S",
            IdAcceso = "",
            AccesoPublico = "",
            AccesoPrivado = ""
        };


        var accesoSimetrico = key.AesKeyBase64AccesoSimetrico;
        var codigoAutenticacionHash = key.HmacKeyBase64CodigoAutentificacionHash;

        accesoSimetrico = Crypto.DecryptAes(accesoSimetricoGlobal, codigoAutenticacionHashGlobal, accesoSimetrico);
        codigoAutenticacionHash = Crypto.DecryptAes(accesoSimetricoGlobal, codigoAutenticacionHashGlobal, codigoAutenticacionHash);

        var a = Crypto.DecryptAes(accesoSimetrico, codigoAutenticacionHash, "1BZQ+V6jQQtYMJslb1GADLJb/nYV0ZUyBCVD1354ALOvaN+Zt3qhKsUL8GW38x7p0p+SHnYlwRpoHc29tfbkuw==");


        var request = GenerarRequest1(accesoSimetrico, codigoAutenticacionHash);
        var json = System.Text.Json.JsonSerializer.Serialize(
            request,
            new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase }
        );
        #endregion
    }
    public static void CifrarRequest(string accesoSimetrico, string codigoAutenticacionHash, CrearPolizaContableRequest request)
    {
        if(request.IdSociedad != "")
            request.IdSociedad = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, request.IdSociedad);
        if(request.ClaseDocumento != "")
            request.ClaseDocumento = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, request.ClaseDocumento);
        if(request.CodigoMoneda != "")
            request.CodigoMoneda = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, request.CodigoMoneda);
        foreach (var detalle in request.DetallesPoliza)
        {
            if(detalle.NumeroCuenta != "")
                detalle.NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, detalle.NumeroCuenta);
            if(detalle.Importe != "")
                detalle.Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, detalle.Importe);
            if (detalle.ImporteImpuesto != "")
                detalle.ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, detalle.ImporteImpuesto);
        }
    }
    public static CrearPolizaContableRequest GenerarRequestPlantilla(string accesoSimetrico, string codigoAutenticacionHash)
    {
        return new CrearPolizaContableRequest
        {
            FechaDocumento = "",
            IdSociedad = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
            //ClaseDocumento = "",
            ClaseDocumento = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
            FechaContable = "",
            Periodo = "",
            TipoCambio = "",
            CodigoMoneda = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
            ReferenciaCabecero = "",
            TextoCabecero = "",
            LlaveSistema = "",
            PrimerNumeroReferencia = "",
            DetallesPoliza =
            [
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "",
                    ClaveContable = "",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "",
                    ClaveContable = "",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                }
            ]
        };
    }
    public static CrearPolizaContableRequest GenerarRequest1(string accesoSimetrico, string codigoAutenticacionHash)
    {
        return new CrearPolizaContableRequest
        {
            FechaDocumento = "19.09.2025",
            IdSociedad = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "3001"),
            ClaseDocumento = Crypto.EncryptAes(accesoSimetrico,codigoAutenticacionHash,"SA"),
            FechaContable = "2025-08-04",
            Periodo = "8",
            TipoCambio = "",
            CodigoMoneda = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "MXN"),
            ReferenciaCabecero = "D08250000000625",
            TextoCabecero = "POLIZAS DE AJUSTE 04.08.2",
            LlaveSistema = "D08250000000625",
            PrimerNumeroReferencia = "D08250000000625",
            DetallesPoliza =
            [
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000001",
                    ClaveContable = "40",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "6200700002"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0.09"),
                    CodigoCentroCostos = "140100",
                    CodigoAsignacion = "",
                    Texto = "POLIZAS DE AJUSTE 04.08.2025 CC 140100",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    CodigoDivision = "",
                    SegundoNumeroReferencia = "0100"
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000002",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1002990000"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0.09"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZAS DE AJUSTE 04.08.2025 CC 140100",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    CodigoDivision = "",
                    SegundoNumeroReferencia = "0100"
                }
            ]
        };
    }
    public static CrearPolizaContableRequest GenerarRequest2(string accesoSimetrico, string codigoAutenticacionHash)
    {
        return new CrearPolizaContableRequest
        {
            FechaDocumento = "19.09.2025",
            IdSociedad = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "3001"),
            //ClaseDocumento = "",
            ClaseDocumento = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1A"),
            FechaContable = "2025-09-18",
            Periodo = "9",
            TipoCambio = "",
            CodigoMoneda = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "MXN"),
            ReferenciaCabecero = "D09250000000340",
            TextoCabecero = "POLIZA DE VENTAS DE CONTA",
            LlaveSistema = "D09250000000340",
            PrimerNumeroReferencia = "D09250000000340",
            DetallesPoliza =
            [
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000001",
                    ClaveContable = "40",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1002990000"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1492.06"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 140100",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = "0100"
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000002",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1083040001"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1010.9"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 140100",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = "0100"
                }
                ,
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000003",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1083040000"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "10.42"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 140100",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = "0100"
                }
                ,
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000004",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "2011000330"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0.05"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 140100",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = "0100"
                }
                ,
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000005",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "2011000330"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0.01"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 140100",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = "0100"
                }
                ,
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000006",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "4000010600"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1477"),
                    CodigoCentroCostos = "140100",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 140100",
                    IndicadorIva = "AF",
                    //ImporteImpuesto = "",
                    ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                }
                ,
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000007",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "4000010500"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "12.93"),
                    CodigoCentroCostos = "140100",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 140100",
                    IndicadorIva = "AD",
                    //ImporteImpuesto = "",
                    ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "2.07"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                }
                ,
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000008",
                    ClaveContable = "40",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "5000010101"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1010.9"),
                    CodigoCentroCostos = "140100",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 140100",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                }
                ,
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000009",
                    ClaveContable = "40",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "5000010200"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "10.42"),
                    CodigoCentroCostos = "140100",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 140100",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                }
            ]
        };
    }
    public static CrearPolizaContableRequest GenerarRequest3(string accesoSimetrico, string codigoAutenticacionHash)
    {
        return new CrearPolizaContableRequest
        {
            FechaDocumento = "19.09.2025",
            IdSociedad = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "3001"),
            //ClaseDocumento = "",
            ClaseDocumento = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1A"),
            FechaContable = "2025-09-18",
            Periodo = "9",
            TipoCambio = "",
            CodigoMoneda = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "MXM"),
            ReferenciaCabecero = "D09250000000340",
            TextoCabecero = "POLIZA DE VENTAS DE CONTA",
            LlaveSistema = "D09250000000340",
            PrimerNumeroReferencia = "D09250000000340",
            DetallesPoliza =
            [
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000001",
                    ClaveContable = "40",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1002990000"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "2761.05"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 142068",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000002",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1083040001"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1572.38"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 142068",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = "2068"
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000003",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1083040000"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "278.62"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 142068",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = "2068"
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000004",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "2011000330"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0.05"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 142068",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000005",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "2011000330"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "125"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 142068",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000006",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "4000010600"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "2215"),
                    CodigoCentroCostos = "142068",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 142068",
                    IndicadorIva = "AF",
                    //ImporteImpuesto = "",
                    ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000007",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "4000010500"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "362.93"),
                    CodigoCentroCostos = "142068",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 142068",
                    IndicadorIva = "AD",
                    //ImporteImpuesto = "",
                    ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "58.07"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000008",
                    ClaveContable = "40",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "5000010101"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1572.38"),
                    CodigoCentroCostos = "142068",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 142068",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000009",
                    ClaveContable = "40",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "5000010200"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "278.62"),
                    CodigoCentroCostos = "142068",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 142068",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                }
            ]
        };
    }
    public static CrearPolizaContableRequest GenerarRequest4(string accesoSimetrico, string codigoAutenticacionHash)
    {
        return new CrearPolizaContableRequest
        {
            FechaDocumento = "19.09.2025",
            IdSociedad = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "3001"),
            //ClaseDocumento = "",
            ClaseDocumento = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1A"),
            FechaContable = "2025-09-18",
            Periodo = "9",
            TipoCambio = "",
            CodigoMoneda = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "MXM"),
            ReferenciaCabecero = "D09250000000341",
            TextoCabecero = "POLIZA DE VENTAS DE CONTA",
            LlaveSistema = "D09250000000341",
            PrimerNumeroReferencia = "D09250000000341",
            DetallesPoliza =
            [
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000001",
                    ClaveContable = "40",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1002990000"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "595.01"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 142503",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000002",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1083040001"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "318.6"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 142503",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = "2503"
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000003",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1083040000"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "71.4"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 142503",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = "2503"
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000004",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "2011000330"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0.01"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 142503",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000005",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "4000010600"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "485"),
                    CodigoCentroCostos = "142503",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 142503",
                    IndicadorIva = "AF",
                    //ImporteImpuesto = "",
                    ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000006",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "4000010500"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "94.83"),
                    CodigoCentroCostos = "142503",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 142503",
                    IndicadorIva = "AD",
                    //ImporteImpuesto = "",
                    ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "15.17"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000007",
                    ClaveContable = "40",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "5000010101"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "318.6"),
                    CodigoCentroCostos = "142503",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 142503",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000008",
                    ClaveContable = "40",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "5000010200"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "71.4"),
                    CodigoCentroCostos = "142503",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 142503",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                }
            ]
        };
    }
    
    public static CrearPolizaContableRequest GenerarRequest5(string accesoSimetrico, string codigoAutenticacionHash)
    {
        return new CrearPolizaContableRequest
        {
            FechaDocumento = "15.08.2025",
            IdSociedad = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "3001"),
            //ClaseDocumento = "",
            ClaseDocumento = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "2R"),
            FechaContable = "2025-07-31",
            Periodo = "7",
            TipoCambio = "",
            CodigoMoneda = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "MXN"),
            ReferenciaCabecero = "D07250000000650",
            TextoCabecero = "POLIZA DE COMPRAS MAYORIS",
            LlaveSistema = "D07250000000650",
            PrimerNumeroReferencia = "D07250000000650",
            DetallesPoliza =
            [
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000001",
                    ClaveContable = "40",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1084010003"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "90665.34"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "3421 31072025 D07250000000650  ",
                    IndicadorIva = "AK",
                    //ImporteImpuesto = "",
                    ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = "3421"
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000002",
                    ClaveContable = "40",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1083040000"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "8097.86"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "3421 31072025 D07250000000650  ",
                    IndicadorIva = "AI",
                    //ImporteImpuesto = "",
                    ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1295.7"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000003",
                    ClaveContable = "31",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "710554"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "48108.88"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "3421 31072025 D07250000000650  710554",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000004",
                    ClaveContable = "31",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "81835381"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "3421 31072025 D07250000000650  81835381",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000005",
                    ClaveContable = "31",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "2005010000"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "28729.64"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "3421 31072025 D07250000000650  ",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000006",
                    ClaveContable = "40",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "9700100002"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "90665.34"),
                    CodigoCentroCostos = "143421",
                    CodigoAsignacion = "",
                    Texto = "3421 31072025 D07250000000650  ",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000007",
                    ClaveContable = "40",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "9700100001"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "8097.86"),
                    CodigoCentroCostos = "143421",
                    CodigoAsignacion = "",
                    Texto = "3421 31072025 D07250000000650  ",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000008",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "9700100003"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "98763.2"),
                    CodigoCentroCostos = "143421",
                    CodigoAsignacion = "",
                    Texto = "3421 31072025 D07250000000650  ",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                }
            ]
        };
    }
    public static CrearPolizaContableRequest GenerarRequest6(string accesoSimetrico, string codigoAutenticacionHash)
    {
        return new CrearPolizaContableRequest
        {
            FechaDocumento = "25.08.2025",
            IdSociedad = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "3001"),
            ClaseDocumento = "",
            //ClaseDocumento = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
            FechaContable = "2025-08-25",
            Periodo = "8",
            TipoCambio = "",
            CodigoMoneda = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "MX"),
            ReferenciaCabecero = "D08250000000471",
            TextoCabecero = "POLIZA DE FACTURACION 25.",
            LlaveSistema = "D08250000000471",
            PrimerNumeroReferencia = "D08250000000471",
            DetallesPoliza =
            [
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000001",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1006040026"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "157727.56"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE FACTURACION 25.08.2025 CC ",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000002",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1006040026"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "9740.49"),
                    CodigoCentroCostos = "POLIZA DE FACTURACION 25.08.2025 CC ",
                    CodigoAsignacion = "",
                    Texto = "",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000003",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1006040026"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "278433.66"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE FACTURACION 25.08.2025 CC ",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000004",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1006040026"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "8938.87"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE FACTURACION 25.08.2025 CC ",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000005",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1006040026"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "29271.95"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE FACTURACION 25.08.2025 CC ",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000006",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1006040026"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "55259.82"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE FACTURACION 25.08.2025 CC ",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000007",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1006040026"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "8068.99"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE FACTURACION 25.08.2025 CC ",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000008",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1006040026"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "8559.87"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE FACTURACION 25.08.2025 CC ",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000009",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1006040026"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "15002.79"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE FACTURACION 25.08.2025 CC ",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000010",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1006040026"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "24237.44"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE FACTURACION 25.08.2025 CC ",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000011",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1006040026"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "16065.92"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE FACTURACION 25.08.2025 CC ",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000012",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1006040026"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "18596.36"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE FACTURACION 25.08.2025 CC ",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000013",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1006040026"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "8938.87"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE FACTURACION 25.08.2025 CC ",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000014",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1006040026"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "60760.78"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE FACTURACION 25.08.2025 CC ",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000015",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1006040026"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "647.5"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE FACTURACION 25.08.2025 CC ",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000016",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1006040026"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "71801.85"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE FACTURACION 25.08.2025 CC ",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000017",
                    ClaveContable = "1",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "2872742"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "772052.72"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE FACTURACION 25.08.2025 CC ",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                }
            ]
        };
    }


    public static CrearPolizaContableRequest GenerarRequest7(string accesoSimetrico, string codigoAutenticacionHash)
    {
        return new CrearPolizaContableRequest
        {
            FechaDocumento = "19.09.2025",
            IdSociedad = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "3001"),
            //ClaseDocumento = "",
            ClaseDocumento = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1A"),
            FechaContable = "2025-09-18",
            Periodo = "9",
            TipoCambio = "",
            CodigoMoneda = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "MXN"),
            ReferenciaCabecero = "D09250000000355",
            TextoCabecero = "POLIZA DE VENTAS DE CONTA",
            LlaveSistema = "D09250000000355",
            PrimerNumeroReferencia = "D09250000000355",
            DetallesPoliza =
            [
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000001",
                    ClaveContable = "40",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1002990000"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1492.06"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 140100",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = "0100"
                },
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000002",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1083040001"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1010.9"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 140100",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = "0100"
                }
                ,
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000003",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1083040000"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "10.42"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 140100",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = "0100"
                }
                ,
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000004",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "2011000330"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0.05"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 140100",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                }
                ,
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000005",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "2011000330"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0.01"),
                    CodigoCentroCostos = "",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 140100",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                }
                ,
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000006",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "4000010600"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1477"),
                    CodigoCentroCostos = "140100",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 140100",
                    IndicadorIva = "AF",
                    //ImporteImpuesto = "",
                    ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "0"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                }
                ,
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000007",
                    ClaveContable = "50",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "4000010500"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "12.93"),
                    CodigoCentroCostos = "140100",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 140100",
                    IndicadorIva = "AD",
                    //ImporteImpuesto = "",
                    ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "2.07"),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                }
                ,
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000008",
                    ClaveContable = "40",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "5000010101"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "1010.9"),
                    CodigoCentroCostos = "140100",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 140100",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                }
                ,
                new DetallePolizaDto
                {
                    NumeroPosicionDocumento = "0000000009",
                    ClaveContable = "40",
                    NumeroCuenta = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "5000010200"),
                    Importe = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, "10.42"),
                    CodigoCentroCostos = "140100",
                    CodigoAsignacion = "",
                    Texto = "POLIZA DE VENTAS DE CONTADO 18.09.2025 CC 140100",
                    IndicadorIva = "",
                    ImporteImpuesto = "",
                    //ImporteImpuesto = Crypto.EncryptAes(accesoSimetrico, codigoAutenticacionHash, ""),
                    CodigoDivision = "",
                    SegundoNumeroReferencia = ""
                }
            ]
        };
    }
}