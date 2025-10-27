namespace ProcesosFundacion.Entidades.Satelite
{

    public partial class Key
    {
        public string AesKeyBase64AccesoSimetrico { get; set; } = string.Empty;
        public string HmacKeyBase64CodigoAutentificacionHash { get; set; } = string.Empty;
        public string IdAcceso { get; set; } = string.Empty;
        public string AccesoPublico { get; set; } = string.Empty;
        public string AccesoPrivado { get; set; } = string.Empty;
    }

    public class ResponseKey
    {
        public string Mensaje { get; set; } = string.Empty;
        public string Folio { get; set; } = string.Empty;
        public ResultadoKey Resultado { get; set; } = new ResultadoKey();
    }

    public class ResultadoKey
    {
        public long IdAcceso { get; set; }
        public string AccesoPublico { get; set; } = string.Empty;
        public string AccesoPrivado { get; set; } = string.Empty;
        public string AccesoSimetrico { get; set; } = string.Empty;
        public string CodigoAutentificacionHash { get; set; } = string.Empty;
        public string CodigoAutenticacionHash { get; set; } = string.Empty;
    }

}

