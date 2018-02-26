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

                if (result.Any() && !result.FirstOrDefault().Used.Value)
                {
                    var user = result.FirstOrDefault();

                    apiReturn.Status = status;
                    apiReturn.Message = "Usuário Autorizado";
                    apiReturn.Object = new
                    {
                        Id = user.Id,
                        PhoneNumber = user.PhoneNumber,
                        Token = user.Token,
                        Logged = true
                    };

                    user.Used = true;

                    _context.AppauthorizationToUse.Update(user);
                    _context.SaveChanges();
                }
                else if (result.FirstOrDefault().Used.Value)
                {
                    apiReturn.Status = status;
                    apiReturn.Message = "Acesso não autorizado. Token já utilizado";
                    apiReturn.Object = null;
                }
                else
                {
                    apiReturn.Status = status;
                    apiReturn.Message = "Acesso não autorizado";
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