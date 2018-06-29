using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaCCB.Data.Models;
using Microsoft.Extensions.Configuration;
using AgendaCCB.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query;
using AgendaCCB.Web.Models;

namespace AgendaCCB.Web.Controllers
{
    public class AppauthorizationToUseController : BaseController
    {
        public AppauthorizationToUseController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {
        }

        // GET: AppauthorizationToUse
        public IActionResult Index()
        {
            var agendaccbContext = _context.AppauthorizationToUse.Include(a => a.UserCreatorNavigation);
            var authorizationToUseVM = MapTo(agendaccbContext);
            return View(authorizationToUseVM);
        }

        // GET: AppauthorizationToUse/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appauthorizationToUse = _context.AppauthorizationToUse
                .Include(a => a.UserCreatorNavigation)
                .SingleOrDefault(m => m.Id == id);

            if (appauthorizationToUse == null)
            {
                return NotFound();
            }

            var authorizationToUseVM = MapTo(appauthorizationToUse);

            return View(authorizationToUseVM);
        }

        // GET: AppauthorizationToUse/Create
        public IActionResult Create()
        {
            AppAuthorizationToUseViewModel appauthorizationToUse = new AppAuthorizationToUseViewModel();            
            return View(appauthorizationToUse);
        }

        // POST: AppauthorizationToUse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PhoneNumber,Token,Created,UserCreator,Used")] AppAuthorizationToUseViewModel appauthorizationToUse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(MapTo(appauthorizationToUse));
                await _context.SaveChangesAsync();
                //SmsSenderExtensions.SendMessage(appauthorizationToUse.PhoneNumber, appauthorizationToUse.Token);
                return RedirectToAction(nameof(Index));
            }
            
            return View(appauthorizationToUse);
        }

        private string GenerateToken()
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvxwyz0123456789";
            return new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // GET: AppauthorizationToUse/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appauthorizationToUse = await _context.AppauthorizationToUse.SingleOrDefaultAsync(m => m.Id == id);
            if (appauthorizationToUse == null)
            {
                return NotFound();
            }

            var authorizationToUseVM = MapTo(appauthorizationToUse);
            return View(authorizationToUseVM);
        }

        // POST: AppauthorizationToUse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PhoneNumber,Token,Created,UserCreator,Used")] AppAuthorizationToUseViewModel appauthorizationToUse)
        {
            if (id != appauthorizationToUse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(MapTo(appauthorizationToUse));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppauthorizationToUseExists(appauthorizationToUse.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserCreator"] = new SelectList(_context.AspNetUsers, "Id", "Id", appauthorizationToUse.UserCreator);
            return View(appauthorizationToUse);
        }

        // GET: AppauthorizationToUse/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appauthorizationToUse = await _context.AppauthorizationToUse
                .Include(a => a.UserCreatorNavigation)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (appauthorizationToUse == null)
            {
                return NotFound();
            }

            return View(MapTo(appauthorizationToUse));
        }

        // POST: AppauthorizationToUse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appauthorizationToUse = await _context.AppauthorizationToUse.SingleOrDefaultAsync(m => m.Id == id);
            _context.AppauthorizationToUse.Remove(appauthorizationToUse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppauthorizationToUseExists(int id)
        {
            return _context.AppauthorizationToUse.Any(e => e.Id == id);
        }

        private List<AppAuthorizationToUseViewModel> MapTo(IIncludableQueryable<AppauthorizationToUse, AspNetUsers> agendaccbContext)
        {
            var ListAuthorization = new List<AppAuthorizationToUseViewModel>();

            foreach (var item in agendaccbContext)
            {
                ListAuthorization.Add(new AppAuthorizationToUseViewModel()
                {
                    Created = item.Created,
                    Id = item.Id,
                    PhoneNumber = item.PhoneNumber,
                    Token = item.Token,
                    Used = item.Used.Value,
                    UserCreator = item.UserCreatorNavigation.UserName
                });
            }

            return ListAuthorization;
        }

        private AppAuthorizationToUseViewModel MapTo(AppauthorizationToUse appauthorizationToUse)
        {
            return new AppAuthorizationToUseViewModel()
            {
                Created = appauthorizationToUse.Created,
                Id = appauthorizationToUse.Id,
                PhoneNumber = appauthorizationToUse.PhoneNumber,
                Token = appauthorizationToUse.Token,
                Used = appauthorizationToUse.Used.Value,
                UserCreator = appauthorizationToUse.UserCreator
            };
        }

        private AppauthorizationToUse MapTo(AppAuthorizationToUseViewModel appauthorizationToUse)
        {
            var token = string.IsNullOrEmpty(appauthorizationToUse.Token) ? GenerateToken() : appauthorizationToUse.Token;
            var userCreator = string.IsNullOrEmpty(appauthorizationToUse.UserCreator) ? _context.AspNetUsers.FirstOrDefault(us => us.Id == _userId).Id : _context.AspNetUsers.FirstOrDefault(us => us.Id == appauthorizationToUse.UserCreator).Id;
            var created = appauthorizationToUse.Created == new DateTime() ? DateTime.Now : appauthorizationToUse.Created;
            return new AppauthorizationToUse()
            {
                Created = created,
                Id = appauthorizationToUse.Id,
                PhoneNumber = appauthorizationToUse.PhoneNumber,
                Token = token,
                Used = appauthorizationToUse.Used,
                UserCreator = userCreator 
            };
        }
    }
}
