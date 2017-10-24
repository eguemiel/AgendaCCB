using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgendaCCB.Data.Models;
using AgendaCCB.Web.Models;
using System;

namespace AgendaCCB.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly agendaccbContext _context;

        public UsersController(agendaccbContext context)
        {
            _context = context;    
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsersViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                var users = this.MapTo(userVM);
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(userVM);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            var userVM = MapTo(users);
            return View(userVM);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UsersViewModel userVM)
        {
            if (id != userVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var users = this.MapTo(userVM);

                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(userVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(userVM);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private Users MapTo(UsersViewModel userVM)
        {
            Users user = null;

            if (userVM.Id != 0)
            {
                user = _context.Users.Find(userVM.Id);
                user.UserName = userVM.UserName.ToLower();
                user.FullUserName = user.FullUserName;
                user.Password = Helpers.EncryptString(userVM.Password);
                user.Email = userVM.Email;
                user.QuestionPassword = userVM.QuestionPassword;
                user.AnswerPassword = userVM.AnswerPassword;
                user.Active = userVM.Active;
            }else
            {
                user = new Users(){
                   UserName = userVM.UserName,
                   FullUserName = userVM.FullUserName,
                   Active = userVM.Active,
                   AnswerPassword = userVM.AnswerPassword,
                   QuestionPassword = userVM.QuestionPassword,
                   Created = DateTime.Now,
                   Password = userVM.Password,
                   Email = userVM.Email
                };
            }
            
            return user;
        }

        private UsersViewModel MapTo(Users user)
        {
            UsersViewModel userVM = new UsersViewModel
            {
                Id = user.Id,
                UserName = user.UserName.ToLower(),
                FullUserName = user.FullUserName,
                Password = Helpers.DecryptString(user.Password),
                ConfirmPassword = Helpers.DecryptString(user.Password),
                Email = user.Email,
                QuestionPassword = user.QuestionPassword,
                AnswerPassword = user.AnswerPassword,
                Blocked = user.Blocked,
                CountFailureResponseFailure = user.CountFailureResponseFailure,
                CountFailurePasswordFailure = user.CountFailurePasswordFailure,
                Created = user.Created,
                LastAccess = user.LastAccess,
                Active = user.Active
            };

            return userVM;
        }
    }
}
