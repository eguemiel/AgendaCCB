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

                var result = _context.AppauthorizationToUse.Where(ap => ap.PhoneNumber == model.PhoneNumber && ap.Token == model.Token);
               
                var apiReturn = new ApiReturn();

                if (result.Any() && !result.FirstOrDefault().Used.Value)
                {
                    var user = result.FirstOrDefault();

                    apiReturn.Status = (int)HttpStatusCode.OK;
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
                else if (result.Any() && result.FirstOrDefault().Used.Value)
                {
                    apiReturn.Status = (int)HttpStatusCode.Unauthorized;
                    apiReturn.Message = "Acesso não autorizado. Token já utilizado";
                    apiReturn.Object = null;
                }
                else
                {
                    apiReturn.Status = (int)HttpStatusCode.Unauthorized;
                    apiReturn.Message = "Acesso não autorizado. Usuário ou senha inválidos";
                    apiReturn.Object = null;
                }

                return new ObjectResult(apiReturn);
            }
            catch (Exception ex)
            {
                var apiRetorno = new ApiReturn() { Status = (int)HttpStatusCode.BadRequest, Object = false, Message = ex.Message };
                return new ObjectResult(apiRetorno);
            }
        }
    }
}