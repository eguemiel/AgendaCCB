using System.Collections.Generic;
using System.Threading.Tasks;
using AgendaCCB.App.Models;

namespace AgendaCCB.App.Services.Commom
{
    public interface IAgendaCCBApiService
    {
        Task<ApiReturn> PostAsync(string urlApi, object body, Dictionary<string, string> headers = null, bool basicAuthentication = false);
        Task<ApiReturn> GetAsync(string urlApi, Dictionary<string, string> headers = null, bool basicAuthentication = false);
    }
}