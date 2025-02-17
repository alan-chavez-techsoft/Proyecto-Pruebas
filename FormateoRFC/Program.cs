namespace FormateoRFC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rfc = "CACC711126D59";
            var rfcFormateado = FormatearRFC(rfc);
            if (rfcFormateado == "CACC-711126-D59")
            {
                Console.WriteLine("RFC formateado correctamente");
            }
            else
            {
                Console.WriteLine("Error al formatear RFC");
            }
            
        }

        public static string FormatearRFC(string rfc)
        {
            var rfcFormateado = rfc.Substring(0, 4) + "-" + rfc.Substring(4, 6) + "-" + rfc.Substring(10, 3);
            return rfcFormateado;
        }
    }
}
