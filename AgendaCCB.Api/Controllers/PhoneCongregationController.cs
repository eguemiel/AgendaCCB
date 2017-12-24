using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AgendaCCB.Api.Authorization;
using AgendaCCB.Api.Models;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace AgendaCCB.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/PhoneCongregation")]
    public class PhoneCongregationController : BaseController
    {
        public PhoneCongregationController(IConfiguration configuration) : base(configuration)
        {
        }

        [BasicAuthentication]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var phoneCongregationsBD = _context.CommonCongregation
                                    .Where(cc => (cc.Address != null && cc.Address != string.Empty) &&
                                                 (cc.PhoneNumber != null && cc.PhoneNumber != string.Empty) ||
                                                 (cc.FaxPhoneNumber != null && cc.FaxPhoneNumber != string.Empty) ||
                                                 (cc.ZipCode != null && cc.ZipCode != string.Empty));

                var phoneCongregations = MapTo(phoneCongregationsBD);

                var apiRetorno = new ApiReturn() { Status = (int)HttpStatusCode.OK, Object = phoneCongregations };

                return new ObjectResult(apiRetorno);
            }
            catch (System.Exception ex)
            {
                var apiRetorno = new ApiReturn() { Status = (int)HttpStatusCode.BadRequest, Message = ex.Message };
                return new ObjectResult(apiRetorno);
            }
        }

        private List<PhoneCongregation> MapTo(IQueryable<Data.Models.CommonCongregation> phoneCongregationsBD)
        {
            List<PhoneCongregation> phoneCongregations = new List<PhoneCongregation>();

            foreach (var phoneCongregation in phoneCongregationsBD)
            {
                phoneCongregations.Add(MapTo(phoneCongregation));
            }

            return phoneCongregations;
        }

        private PhoneCongregation MapTo(Data.Models.CommonCongregation phoneCongregationBD)
        {
            if (phoneCongregationBD == null)
                return null;           

            var phoneNumber = new PhoneCongregation()
            {
               Name = phoneCongregationBD.Name,
               Address = phoneCongregationBD.Address,
               City = phoneCongregationBD.CityNavigation.Name,
               District = phoneCongregationBD.District,
               FaxPhoneNumber = phoneCongregationBD.FaxPhoneNumber,
               PhoneNumber = phoneCongregationBD.PhoneNumber,
               ZipCode = phoneCongregationBD.ZipCode
            };

            return phoneNumber;
        }
    }
}