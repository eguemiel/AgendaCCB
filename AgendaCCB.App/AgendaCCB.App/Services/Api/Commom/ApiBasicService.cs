using AgendaCCB.App.Models;
using AgendaCCB.App.Services.AppServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AgendaCCB.App.Services.Api.Common
{
    public class ApiBasicService : IApiBasicService
    {
        private const string BaseUrl = "http://agendaccbapi.azurewebsites.net/api/";
        public const string BasicAuthHeader = "Basic YWdlbmRhRGVmYXVsdDpISkR0QDJ5RW5JJDQmWkRjKXRBcm00";

        public static Action ComportamentoSessaoExpirada { get; set; } = () => { };
        public bool TentarRelogarUsuario { get; set; }

        public ApiBasicService()
        {
            TentarRelogarUsuario = true;
        }

        public async Task<ApiReturn> PostAsync(string urlApi, object body, Dictionary<string, string> headers = null, bool basicAuthentication = false)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaders(client, headers, basicAuthentication);

                var requestUri = $"{BaseUrl}{urlApi}";

                HttpResponseMessage responseMessage = null;

                try
                {
                    StringContent data = new StringContent("");
                    if (body != null)
                    {
                        string json = JsonConvert.SerializeObject(body);
                        data = new StringContent(json, Encoding.UTF8, "application/json");
                    }

                    responseMessage = await client.PostAsync(requestUri, data).ConfigureAwait(false);

                    if (responseMessage.StatusCode == HttpStatusCode.Unauthorized && !basicAuthentication && TentarRelogarUsuario)
                    {
                        bool refreshSuccess = await new UserService().TryRefreshToken();

                        if (refreshSuccess)
                        {
                            using (var clientRetry = new HttpClient())
                            {
                                PrepareHeaders(clientRetry, headers, basicAuthentication);
                                responseMessage = await client.PostAsync(requestUri, data).ConfigureAwait(false);
                            }
                        }
                        else
                        {
                            new UserService().Logout();
                            Device.BeginInvokeOnMainThread(ApiBasicService.ComportamentoSessaoExpirada);
                            return null;
                        }
                    }

                    using (var responseStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        return JsonConvert.DeserializeObject<ApiReturn>(
                            await new StreamReader(responseStream)
                                .ReadToEndAsync().ConfigureAwait(false));
                    }
                }
                catch (Exception ex)
                {
                    if (responseMessage == null)
                    {
                        responseMessage = new HttpResponseMessage();
                    }

                    responseMessage.StatusCode = HttpStatusCode.InternalServerError;
                    responseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest Erro: {0}", ex);
                }

                return null;
            }
        }

        public async Task<ApiReturn> GetAsync(string urlApi, Dictionary<string, string> headers = null, bool basicAuthentication = false)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaders(client, headers, basicAuthentication);

                var requestUri = $"{BaseUrl}{urlApi}";

                HttpResponseMessage responseMessage = null;

                try
                {
                    responseMessage = await client.GetAsync(requestUri).ConfigureAwait(false);                    

                    if (responseMessage.StatusCode == HttpStatusCode.Unauthorized && !basicAuthentication && TentarRelogarUsuario)
                    {
                        bool refreshSuccess = await new UserService().TryRefreshToken();

                        if (refreshSuccess)
                        {
                            using (var clientRetry = new HttpClient())
                            {
                                PrepareHeaders(clientRetry, headers, basicAuthentication);
                                responseMessage = await clientRetry.GetAsync(requestUri).ConfigureAwait(false);
                            }
                        }
                        else
                        {
                            new UserService().Logout();
                            Device.BeginInvokeOnMainThread(ApiBasicService.ComportamentoSessaoExpirada);
                            return null;
                        }
                    }

                    using (var responseStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        return JsonConvert.DeserializeObject<ApiReturn>(
                            await new StreamReader(responseStream)
                                .ReadToEndAsync().ConfigureAwait(false));
                    }
                }
                catch (System.Exception ex)
                {
                    if (responseMessage == null)
                    {
                        responseMessage = new HttpResponseMessage();
                    }

                    responseMessage.StatusCode = HttpStatusCode.InternalServerError;
                    responseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest Erro: {0}", ex);
                }

                return null;
            }
        }

        private void PrepareHeaders(HttpClient client, Dictionary<string, string> headers, bool basicAuthentication)
        {
            string authKey = "Authorization";

            if (headers == null)
                headers = new Dictionary<string, string>();

            if (headers.ContainsKey(authKey))
                headers.Remove(authKey);

            if (basicAuthentication)
            {
                headers.Add(authKey, BasicAuthHeader);
            }
            else
            {
                var UserService = new UserService();
                UserAppSession usuarioAppSessao = UserService.GetUsuario(true);

                if (!string.IsNullOrEmpty(usuarioAppSessao.Token))
                {
                    headers.Add(authKey, $"Bearer {usuarioAppSessao.Token}");
                }
            }

            foreach (var item in headers)
            {
                if (client.DefaultRequestHeaders.Contains(item.Key))
                    client.DefaultRequestHeaders.Remove(item.Key);

                client.DefaultRequestHeaders.Add(item.Key, item.Value);
            }
        }
       
    }
}
