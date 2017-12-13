using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AgendaCCB.Api.Models;
using System.Net;
using AgendaCCB.Api.Authorization;

namespace AgendaCCB.Api.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : BaseController
    {
        public LoginController(IConfiguration configuration) : base(configuration)
        {
        }

        [BasicAuthentication]
        [HttpPost]
        public IActionResult Login([FromBody]LoginModel model)
        {
            try
            {
                if (model == null)
                    throw new Exception();

                var result = _context.AppauthorizationToUse.Where(ap => ap.PhoneNumber == model.PhoneNumber && ap.Token == model.Token && !ap.Used.Value);
                var status = result.Any() ? (int)HttpStatusCode.OK : (int)HttpStatusCode.Unauthorized;

                var apiReturn = new ApiReturn();

                if(result.Any())
                {
                    var user = result.FirstOrDefault();
                    apiReturn.Status = status;
                    apiReturn.Message = "User Authorized";
                    apiReturn.Object = new
                    {
                        Id = user.Id,
                        PhoneNumber = user.PhoneNumber,
                        Token = user.Token,                        
                        Logged = true
                    };
                }else
                {
                    apiReturn.Status = status;
                    apiReturn.Message = "User Unauthorized";
                    apiReturn.Object = null;
                }

                return new ObjectResult(apiReturn);
            }
            catch (Exception)
            {
                var apiRetorno = new ApiReturn() { Status = (int)HttpStatusCode.BadRequest, Object = false };
                return new ObjectResult(apiRetorno);
            }
        }
    }
}