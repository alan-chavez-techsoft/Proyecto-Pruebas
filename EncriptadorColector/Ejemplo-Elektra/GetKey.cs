using Newtonsoft.Json;
using ProcesosFundacion.Entidades.Satelite;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
namespace ProcesosFundacion.Tools.Utilerias
{
    public class GetKey
    {
        //private const string urlBaseApiQA = "";
        //private const string UserTokenQA = "";
        //private const string PassTokenQA = "";
        //private const string aesKeyBase64 = "";
        //private const string hmacKeyBase64 = "";
        //public AccessRequest getAccesTokenQA()
        //{
        //    AccessRequest tokenBAZ = new AccessRequest();
        //    try
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            ServicePointManager.Expect100Continue = true;
        //            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //            ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };
        //            var content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("grant_type", "client_credentials") });
        //            byte[] bytes = Encoding.ASCII.GetBytes(UserTokenQA + ":" + PassTokenQA);
        //            string resultContent = "";
        //            client.BaseAddress = new Uri(urlBaseApiQA);
        //            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));
        //            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

        //            var result = client.PostAsync(requestUri: "/oauth2/v1/token", content);
        //            resultContent = result.Result.Content.ReadAsStringAsync().Result;
        //            tokenBAZ = JsonConvert.DeserializeObject<AccessRequest>(resultContent);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return tokenBAZ;
        //}


        //public Key GetKeyQA(string Token)
        //{
        //    Crypto crypto = new Crypto();
        //    Key keyModel = new Key
        //    {
        //        AesKeyBase64AccesoSimetrico = aesKeyBase64,
        //        HmacKeyBase64CodigoAutentificacionHash = hmacKeyBase64
        //    };

        //    try
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            client.DefaultRequestHeaders.Clear();
        //            client.BaseAddress = new Uri(urlBaseApiQA);
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
        //            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: Token);

        //            var response = client.GetAsync(requestUri: "/backoffice/seguridad/v1/aplicaciones/llaves").Result;

        //            if (response.IsSuccessStatusCode)
        //            {
        //                var resultCredentials = response.Content.ReadAsStringAsync().Result;
        //                var responseKey = JsonConvert.DeserializeObject<ResponseKey>(resultCredentials);
        //                var key = new Key
        //                {
        //                    AesKeyBase64AccesoSimetrico = Crypto.DecryptAes(keyModel, responseKey.Resultado.AccesoSimetrico),
        //                    HmacKeyBase64CodigoAutentificacionHash = Crypto.DecryptAes(keyModel, responseKey.Resultado.CodigoAutentificacionHash),
        //                    IdAcceso = responseKey.Resultado.IdAcceso.ToString(),
        //                    AccesoPublico = Crypto.DecryptAes(keyModel, responseKey.Resultado.AccesoPublico)
        //                };

        //                return key;
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return null;
        //}
    }
}