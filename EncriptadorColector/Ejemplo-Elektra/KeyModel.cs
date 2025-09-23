namespace ProcesosFundacion.Entidades.Satelite
{

    public partial class Key
    {
        public string AesKeyBase64AccesoSimetrico { get; set; }
        public string HmacKeyBase64CodigoAutentificacionHash { get; set; }
        public string IdAcceso { get; set; }
        public string AccesoPublico { get; set; }
        public string AccesoPrivado { get; set; }
    }

    public class ResponseKey
    {
        public string Mensaje { get; set; }
        public string Folio { get; set; }
        public ResultadoKey Resultado { get; set; }
    }

    public class ResultadoKey
    {
        public long IdAcceso { get; set; }
        public string AccesoPublico { get; set; }
        public string AccesoPrivado { get; set; }
        public string AccesoSimetrico { get; set; }
        public string CodigoAutentificacionHash { get; set; }
        public string CodigoAutenticacionHash { get; set; }
    }

}

