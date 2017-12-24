using AgendaCCB.App.Helpers;
using AgendaCCB.App.Models;
using AgendaCCB.App.Services.Commom;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AgendaCCB.App.Services.Api
{
    public class ApiUsefulPhoneService : AgendaCCBApiService
    {
        public async Task<IList<UsefulPhone>> GetAllUsefulPhones()
        {
            try
            {
                IList<UsefulPhone> UsefulPhones = new List<UsefulPhone>();
                ApiReturn apiReturn = await GetAsync(String.Format(UrlApi.GetUsefulPhones));

                if (apiReturn.Status == (int)HttpStatusCode.OK)
                {
                    UsefulPhones = JsonConvert.DeserializeObject<IList<UsefulPhone>>(apiReturn.Object.ToString());
                }

                return UsefulPhones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
