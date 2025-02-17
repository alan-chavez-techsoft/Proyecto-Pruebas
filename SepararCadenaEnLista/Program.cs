namespace SepararCadenaEnLista
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cadena = "Alan,Antonio,Andres,Kiyoshi,Martin,Luis";
            var lista = cadena.Split(",").ToList();
            foreach (var nombre in lista)
            {
                Console.WriteLine(nombre);
            }
        }
    }
}
