using Microsoft.AspNetCore.Mvc;
using AgendaCCB.Data.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace AgendaCCB.Web.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        IConfiguration _configuration;

        public readonly agendaccbContext _context;

        public BaseController(IConfiguration configuration)
        {
            _configuration = configuration;
            _context = CreateDbContext();
        }

        public agendaccbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<agendaccbContext>();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));

            return new agendaccbContext(optionsBuilder.Options);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
    }
}