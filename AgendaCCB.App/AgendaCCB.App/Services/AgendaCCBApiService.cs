using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AgendaCCB.App.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

[assembly: Dependency(typeof(AgendaCCB.App.Services.AgendaCCBApiService))]
namespace AgendaCCB.App.Services
{
    public class AgendaCCBApiService : IAgendaCCBApiService
    {
        private const string BaseUrl = "http://agendaccbapi.azurewebsites.net/api/";
       
        public async Task<List<Collaborator>> GetAllCollaborators()
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.GetAsync($"{BaseUrl}Collaborator").ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        return JsonConvert.DeserializeObject<List<Collaborator>>(
                            await new StreamReader(responseStream)
                                .ReadToEndAsync().ConfigureAwait(false));
                    }
                }

                return new List<Collaborator>() {
                    new Collaborator()
                    {
                        Id = 1,
                        Name= "Eguemiel Miquelin Junior",
                        CommumCongregation = "Campo Belo"
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Collaborator> GetCollaboratorById(int id)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.GetAsync($"{BaseUrl}Collaborator/{id}").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    return JsonConvert.DeserializeObject<Collaborator>(
                        await new StreamReader(responseStream)
                            .ReadToEndAsync().ConfigureAwait(false));
                }
            }

            return null;
        }
    }
}
