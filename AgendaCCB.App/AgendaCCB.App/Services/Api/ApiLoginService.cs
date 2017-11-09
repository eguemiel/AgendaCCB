using System.Threading.Tasks;
using Newtonsoft.Json;
using AgendaCCB.App.Services.Api.Common;
using AgendaCCB.App.Models;
using AgendaCCB.App.Helpers;

namespace AgendaCCB.App.Services.Api
{
    public class ApiLoginService : ApiBasicService
    {
        public async Task<ApiReturn<UserAppSessionSerializeModel>> Login(string user, string password)
        {
            ApiReturn apiRetorno = await PostAsync(UrlApi.Login, new { Login = user, Senha = password }, basicAuthentication: true);
            UserAppSessionSerializeModel model = null;

            if (apiRetorno.Success)
            {
                model = JsonConvert.DeserializeObject<UserAppSessionSerializeModel>(apiRetorno.Object.ToString());                   
            }

            return new ApiReturn<UserAppSessionSerializeModel>(apiRetorno, model);
        }
    }
}
