string url = "http://archivingtst.bisoft.com.mx:8028/application";
var uri = new UriBuilder(url);
Console.WriteLine($"Esquema: {uri.Scheme}");
Console.WriteLine($"Dominio: {uri.Host}");
Console.WriteLine($"Puerto: {uri.Port}");
Console.WriteLine($"Camino: {uri.Path}");
Console.WriteLine($"Url: {uri.Uri}");
Console.WriteLine("--------------------------------------------------");
var uri2 = new UriBuilder("archivingtst.bisoft.com.mx")
{
    Scheme = "http",
    Port = 8028
};
Console.WriteLine($"Esquema: {uri2.Scheme}");
Console.WriteLine($"Dominio: {uri2.Host}");
Console.WriteLine($"Puerto: {uri2.Port}");
Console.WriteLine($"Camino: {uri2.Path}");
Console.WriteLine($"Url: {uri2.Uri}");
