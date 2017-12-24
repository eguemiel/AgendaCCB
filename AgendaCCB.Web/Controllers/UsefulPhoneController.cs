using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaCCB.Data.Models;
using Microsoft.Extensions.Configuration;

namespace AgendaCCB.Web.Controllers
{
    public class UsefulPhoneController : BaseController
    {
        public UsefulPhoneController(IConfiguration configuration) : base(configuration)
        {
        }

        // GET: UsefulPhone
        public async Task<IActionResult> Index()
        {
            return View(await _context.UsefulPhone.ToListAsync());
        }

        // GET: UsefulPhone/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usefulPhone = await _context.UsefulPhone
                .SingleOrDefaultAsync(m => m.Id == id);
            if (usefulPhone == null)
            {
                return NotFound();
            }

            return View(usefulPhone);
        }

        // GET: UsefulPhone/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsefulPhone/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LocalName,PhoneNumber")] UsefulPhone usefulPhone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usefulPhone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usefulPhone);
        }

        // GET: UsefulPhone/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usefulPhone = await _context.UsefulPhone.SingleOrDefaultAsync(m => m.Id == id);
            if (usefulPhone == null)
            {
                return NotFound();
            }
            return View(usefulPhone);
        }

        // POST: UsefulPhone/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LocalName,PhoneNumber")] UsefulPhone usefulPhone)
        {
            if (id != usefulPhone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usefulPhone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsefulPhoneExists(usefulPhone.Id))
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
            return View(usefulPhone);
        }

        // GET: UsefulPhone/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usefulPhone = await _context.UsefulPhone
                .SingleOrDefaultAsync(m => m.Id == id);
            if (usefulPhone == null)
            {
                return NotFound();
            }

            return View(usefulPhone);
        }

        // POST: UsefulPhone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usefulPhone = await _context.UsefulPhone.SingleOrDefaultAsync(m => m.Id == id);
            _context.UsefulPhone.Remove(usefulPhone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsefulPhoneExists(int id)
        {
            return _context.UsefulPhone.Any(e => e.Id == id);
        }
    }
}
