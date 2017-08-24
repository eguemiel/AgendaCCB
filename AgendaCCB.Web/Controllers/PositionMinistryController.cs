using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaCCB.Data.Models;

namespace AgendaCCB.Web.Controllers
{
    public class PositionMinistryController : Controller
    {
        private readonly agendaccbContext _context;

        public PositionMinistryController(agendaccbContext context)
        {
            _context = context;    
        }

        // GET: PositionMinistry
        public async Task<IActionResult> Index()
        {
            return View(await _context.PositionMinistry.ToListAsync());
        }

        // GET: PositionMinistry/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var positionMinistry = await _context.PositionMinistry
                .SingleOrDefaultAsync(m => m.Id == id);
            if (positionMinistry == null)
            {
                return NotFound();
            }

            return View(positionMinistry);
        }

        // GET: PositionMinistry/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PositionMinistry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,IsMinistry")] PositionMinistry positionMinistry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(positionMinistry);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(positionMinistry);
        }

        // GET: PositionMinistry/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var positionMinistry = await _context.PositionMinistry.SingleOrDefaultAsync(m => m.Id == id);
            if (positionMinistry == null)
            {
                return NotFound();
            }
            return View(positionMinistry);
        }

        // POST: PositionMinistry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,IsMinistry")] PositionMinistry positionMinistry)
        {
            if (id != positionMinistry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(positionMinistry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PositionMinistryExists(positionMinistry.Id))
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
            return View(positionMinistry);
        }

        // GET: PositionMinistry/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var positionMinistry = await _context.PositionMinistry
                .SingleOrDefaultAsync(m => m.Id == id);
            if (positionMinistry == null)
            {
                return NotFound();
            }

            return View(positionMinistry);
        }

        // POST: PositionMinistry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var positionMinistry = await _context.PositionMinistry.SingleOrDefaultAsync(m => m.Id == id);
            _context.PositionMinistry.Remove(positionMinistry);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PositionMinistryExists(int id)
        {
            return _context.PositionMinistry.Any(e => e.Id == id);
        }
    }
}
