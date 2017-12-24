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
    public class ApiPhoneCongregationService : AgendaCCBApiService
    {
        public async Task<IList<PhoneCongregation>> GetAllPhoneCongregations()
        {
            try
            {
                IList<PhoneCongregation> PhoneCongregations = new List<PhoneCongregation>();
                ApiReturn apiReturn = await GetAsync(String.Format(UrlApi.GetPhoneCongregation));

                if (apiReturn.Status == (int)HttpStatusCode.OK)
                {
                    PhoneCongregations = JsonConvert.DeserializeObject<IList<PhoneCongregation>>(apiReturn.Object.ToString());
                }

                return PhoneCongregations;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
