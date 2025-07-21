namespace Raptor.ScriptAnalyzerConsola.Dtos
{
    public class AnalizarScriptsResponse
    {
        public List<InformacionScript> Scripts { get; set; } = [];
    }
    public class InformacionScript
    {
        public string Nombre { get; set; } = string.Empty;
        public int Orden { get; set; }
        public List<ObjetosAnalizados> Objetos { get; set; } = [];
    }
    public class ObjetosAnalizados
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }
}
