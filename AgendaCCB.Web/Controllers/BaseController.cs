using Microsoft.AspNetCore.Mvc;
using AgendaCCB.Data.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AgendaCCB.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public readonly agendaccbContext _context = new agendaccbContext();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
    }
}