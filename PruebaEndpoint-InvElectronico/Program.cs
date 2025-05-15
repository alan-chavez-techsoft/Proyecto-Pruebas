using Newtonsoft.Json;
using PruebaEndpoint_InvElectronico.Dtos;
using System.Net.Http.Headers;
using System.Text;

internal class Program
{
    private static HttpClient _client = new HttpClient();
    private static Guid _acuseId;
    private static async Task Main(string[] args)
    {
        //client.BaseAddress = new Uri("http://localhost:5083/");
        _client.BaseAddress = new Uri("https://archivingtst.bisoft.com.mx:8080/pharmapickingPS/");
        await Login();
        await ConsultarAcuse();

        Console.WriteLine(_acuseId);
        await InactivarAcuse();
    }
    private static async Task Login()
    {
        var login = new LoginRequest
        {
            Login = "Daya",
            Password = "123"
        };
        var request = JsonConvert.SerializeObject(login);
        HttpContent content = new StringContent(request, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync($"login?MACId=Mac-generico&nombreDispositivo=nombreDispositivo", content);
        var json = await response.Content.ReadAsStringAsync();
        var token = JsonConvert.DeserializeObject<LoginResponse>(json);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
    }
    private static async Task ConsultarAcuse()
    {
        var consultaAcuse = await _client.GetAsync("api/inventarios-electronicos");
        var jsonAcuse = await consultaAcuse.Content.ReadAsStringAsync();
        var acuse = JsonConvert.DeserializeObject<ConsultarInventarioElectronicoResponse>(jsonAcuse);
        _acuseId = acuse.AcuseId;
    }
    private static async Task InactivarAcuse()
    {

        var json = JsonConvert.SerializeObject(_acuseId);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        var responsePerron =
        await _client.PutAsync($"api/inventarios-electronicos/{_acuseId}/inactivar", content);

        //var responsePerron =
        //    await _client.PutAsync($"api/inventarios-electronicos/{_acuseId}/inactivar", null);

        Console.WriteLine(responsePerron);
    }
}