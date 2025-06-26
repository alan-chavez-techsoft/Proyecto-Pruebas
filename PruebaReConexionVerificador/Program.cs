using Newtonsoft.Json;
using PruebaReConexionVerificador;
using System.Net.Http.Headers;
using System.Text;

internal class Program
{
    private static async Task Main(string[] args)
	{
		try
		{
            await Login();
            await ObtenerSucursales();
        }
		catch (UnauthorizedAccessException)
		{
			await Login();
        }
        catch (Exception ex)
		{
            Console.WriteLine(ex.Message);
		}
    }
	private static async Task Login()
	{
		HttpClient _client = new HttpClient();
		_client.BaseAddress = new Uri("https://localhost:7268/");

        var login = new LoginRequest
        {
            Login = "557MARTIN",
            Password = "557MCA"
        };
        var request = JsonConvert.SerializeObject(login);
        HttpContent content = new StringContent(request, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync($"login", content);
        var json = await response.Content.ReadAsStringAsync();
        var token = JsonConvert.DeserializeObject<LoginResponse>(json);
        Console.WriteLine(token.Token);
    }
    private static async Task ObtenerSucursales()
    {
        HttpClient _client = new HttpClient();
        _client.BaseAddress = new Uri("https://localhost:7268/");
        var response = await _client.GetAsync($"api/sucursales");
        if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            throw new UnauthorizedAccessException("Simulando una excepción de acceso no autorizado para probar la reconexión.");
    }
}
