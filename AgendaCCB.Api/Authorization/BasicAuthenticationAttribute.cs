using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Http;

namespace AgendaCCB.Api.Authorization
{
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            try
            {
                if (!actionContext.ModelState.IsValid)
                {
                    actionContext.Result = new BadRequestObjectResult(actionContext.ModelState);
                }

                string authHeader = actionContext.HttpContext.Request.Headers["Authorization"];

                if (!authHeader.StartsWith("Basic"))
                {
                    actionContext.Result = new ObjectResult(new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized));
                    actionContext.HttpContext.Response.Headers.Add("Exception", "Unauthorized");
                }
                else
                {
                    string username = BasicAuthenticationHelper.GetUserName(actionContext);
                    string password = BasicAuthenticationHelper.GetPassword(actionContext);

                    if (IsUserEnabled(username, password))
                    {
                        base.OnActionExecuting(actionContext);
                    }
                    else
                    {
                        actionContext.Result = new ObjectResult(new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized));
                        actionContext.HttpContext.Response.Headers.Add("Exception", "Unauthorized");
                    }
                }
            }
            catch
            {
                actionContext.Result = new ObjectResult(new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized));
                actionContext.HttpContext.Response.Headers.Add("Exception", "Unauthorized");
            }
        }

        private bool IsUserEnabled(string username, string password)
        {
            if (username == "agendaDefault" && password == "HJDt@2yEnI$4&ZDc)tArm4")
                return true;

            return false;
        }
    }
}