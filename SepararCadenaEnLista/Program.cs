using System.Text;

namespace SepararCadenaEnLista
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var cadena = "Alan,Antonio,Andres,Kiyoshi,Martin,Luis";
            //var lista = cadena.Split(",").ToList();
            //foreach (var nombre in lista)
            //{
            //    Console.WriteLine(nombre);
            //}

            string rutaArchivo = @"\\LAP-RUTH\Scripts";

            if(!Directory.Exists(rutaArchivo))
            {
                Console.WriteLine("La ruta no existe o no podemos acceder a ellos");
                return;
            }

            var contenidoJson = Directory.GetFiles(rutaArchivo);
            var file = File.ReadAllText(contenidoJson[0]);
            Console.WriteLine(file);
        }
    }
}
