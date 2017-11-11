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

                var success = _context.AppauthorizationToUse.Where(ap => ap.PhoneNumber == model.PhoneNumber && ap.Token == ap.Token && !ap.Used.Value).Any();
                var status = success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.Unauthorized;

                var apiRetorno = new ApiReturn() { Status = status, Object = success };

                return new ObjectResult(apiRetorno);
            }
            catch (Exception)
            {
                var apiRetorno = new ApiReturn() { Status = (int)HttpStatusCode.BadRequest, Object = false };
                return new ObjectResult(apiRetorno);
            }
        }
    }
}