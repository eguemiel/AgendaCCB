using AgendaCCB.App.Helpers;
using AgendaCCB.App.Models;
using AgendaCCB.App.Services.Commom;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace AgendaCCB.App.Services.Api
{
    public class ApiUsefulPhoneService : AgendaCCBApiService
    {
        public async Task<ApiReturn<IList<UsefulPhone>>> GetAllUsefulPhones()
        {
            try
            {
                IList<UsefulPhone> UsefulPhones = new List<UsefulPhone>();
                ApiReturn apiReturn = await GetAsync(String.Format(UrlApi.GetUsefulPhones));

                if (apiReturn.Status == (int)HttpStatusCode.OK)
                {
                    UsefulPhones = JsonConvert.DeserializeObject<IList<UsefulPhone>>(apiReturn.Object.ToString());
                }

                return new ApiReturn<IList<UsefulPhone>>(apiReturn, UsefulPhones);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
