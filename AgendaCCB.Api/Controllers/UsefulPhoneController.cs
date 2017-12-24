using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AgendaCCB.Api.Authorization;
using AgendaCCB.Api.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AgendaCCB.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsefulPhoneController : BaseController
    {
        public UsefulPhoneController(IConfiguration configuration) : base(configuration)
        {
        }

        [BasicAuthentication]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var usefulPhoneBD = _context.UsefulPhone.ToListAsync();

                var usefulPhones = MapTo(usefulPhoneBD.Result);

                var apiRetorno = new ApiReturn() { Status = (int)HttpStatusCode.OK, Object = usefulPhones };

                return new ObjectResult(apiRetorno);
            }
            catch (System.Exception ex)
            {
                var apiRetorno = new ApiReturn() { Status = (int)HttpStatusCode.BadRequest, Message = ex.Message };
                return new ObjectResult(apiRetorno);
            }
        }

        private List<UsefulPhone> MapTo(List<Data.Models.UsefulPhone> usefulPhoneBD)
        {
            List<UsefulPhone> usefulPhoneList = new List<UsefulPhone>();

            foreach (var usefulPhone in usefulPhoneBD)
            {
                usefulPhoneList.Add(MapTo(usefulPhone));
            }

            return usefulPhoneList;
        }

        private UsefulPhone MapTo(Data.Models.UsefulPhone usefulPhoneBD)
        {
            return new UsefulPhone
            {
                LocalName = usefulPhoneBD.LocalName,
                PhoneNumber = usefulPhoneBD.PhoneNumber
            };
        }
    }
}