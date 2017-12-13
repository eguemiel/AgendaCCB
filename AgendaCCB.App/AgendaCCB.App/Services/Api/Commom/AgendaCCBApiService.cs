using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AgendaCCB.App.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Text;
using System.Net;
using AgendaCCB.App.Services.AppServices;


namespace AgendaCCB.App.Services.Commom
{
    public class AgendaCCBApiService : IAgendaCCBApiService
    {
        //public const string BaseUrl = "http://10.0.2.2:9998/api/";
        public const string BaseUrl = "http://agendaccbapi.azurewebsites.net/api/";
        public const string BasicAuthHeader = "Basic YWdlbmRhRGVmYXVsdDpISkR0QDJ5RW5JJDQmWkRjKXRBcm00";

        public static Action BehaviorIfNotLogged { get; set; } = () => { };
        public bool TryLoginUser { get; set; }

        public AgendaCCBApiService()
        {
            TryLoginUser = true;
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

                    if (responseMessage.StatusCode == HttpStatusCode.Unauthorized && !basicAuthentication && TryLoginUser)
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
                            Device.BeginInvokeOnMainThread(AgendaCCBApiService.BehaviorIfNotLogged);
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

        public async Task<ApiReturn> GetAsync(string urlApi, Dictionary<string, string> headers = null, bool basicAuthentication = true)
        {
            using (var client = new HttpClient())
            {
                PrepareHeaders(client, headers, basicAuthentication);

                var requestUri = $"{BaseUrl}{urlApi}";

                HttpResponseMessage responseMessage = null;

                try
                {
                    responseMessage = await client.GetAsync(requestUri).ConfigureAwait(false);

                    if (responseMessage.StatusCode == HttpStatusCode.Unauthorized && !basicAuthentication && TryLoginUser)
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
                            Device.BeginInvokeOnMainThread(AgendaCCBApiService.BehaviorIfNotLogged);
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
                var userService = new UserService();
                UserAppSession userAppSession = userService.GetUsuario(true);

                if (!string.IsNullOrEmpty(userAppSession.Token))
                {
                    headers.Add(authKey, $"Bearer {userAppSession.Token}");
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
