using AgendaCCB.App.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaCCB.App.Services.Api.Common
{
    public interface IApiBasicService
    {
        Task<ApiReturn> PostAsync(string urlApi, object body, Dictionary <string, string> headers = null, bool basicAuthentication = false);
        Task<ApiReturn> GetAsync(string urlApi, Dictionary<string, string> headers = null, bool basicAuthentication = false);
    }
}
