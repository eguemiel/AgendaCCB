using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Text;
using System.Web.Http.Controllers;

namespace AgendaCCB.Api.Authorization
{
    public static class BasicAuthenticationHelper
    {
        public static string GetUserName(ActionExecutingContext actionContext)
        {
            string authHeader = actionContext.HttpContext.Request.Headers["Authorization"];
            if (authHeader == null || !authHeader.StartsWith("Basic"))
                return null;

            string encodedUserPW = authHeader.Substring("Basic ".Length).Trim();
            string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUserPW));
            string username = decodedToken.Substring(0, decodedToken.IndexOf(":"));
            return username;            
        }

        public static string GetPassword(ActionExecutingContext actionContext)
        {
            string authHeader = actionContext.HttpContext.Request.Headers["Authorization"];
            if (authHeader == null || !authHeader.StartsWith("Basic"))
                return null;

            string encodedUserPW = authHeader.Substring("Basic ".Length).Trim();
            string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUserPW));
            string password = decodedToken.Substring(decodedToken.IndexOf(":") + 1);
            return password;
        }
    }
}