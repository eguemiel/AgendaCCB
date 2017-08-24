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
    public class CollaboratorController : Controller
    {
        private readonly agendaccbContext _context;

        public CollaboratorController(agendaccbContext context)
        {
            _context = context;    
        }

        // GET: Collaborator
        public async Task<IActionResult> Index()
        {
            var agendaccbContext = _context.Collaborator.Include(c => c.IdCommonCongregationNavigation).Include(c => c.IdPositionMinistyNavigation);
            return View(await agendaccbContext.ToListAsync());
        }

        // GET: Collaborator/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaborator = await _context.Collaborator
                .Include(c => c.IdCommonCongregationNavigation)
                .Include(c => c.IdPositionMinistyNavigation)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (collaborator == null)
            {
                return NotFound();
            }

            return View(collaborator);
        }

        // GET: Collaborator/Create
        public IActionResult Create()
        {
            ViewBag.CommonList = new SelectList(_context.CommonCongregation, "Id", "Name");
            ViewBag.PositionMinistryList = new SelectList(_context.PositionMinistry, "Id", "Description");
            return View();
        }

        // POST: Collaborator/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IdPositionMinisty,IdPhoneNumber,IdCommonCongregation")] Collaborator collaborator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(collaborator);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["IdCommonCongregation"] = new SelectList(_context.CommonCongregation, "Id", "Name", collaborator.IdCommonCongregation);
            ViewData["IdPositionMinisty"] = new SelectList(_context.PositionMinistry, "Id", "Description", collaborator.IdPositionMinisty);
            return View(collaborator);
        }

        // GET: Collaborator/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaborator = await _context.Collaborator.SingleOrDefaultAsync(m => m.Id == id);
            if (collaborator == null)
            {
                return NotFound();
            }
            ViewData["IdCommonCongregation"] = new SelectList(_context.CommonCongregation, "Id", "Name", collaborator.IdCommonCongregation);
            ViewData["IdPositionMinisty"] = new SelectList(_context.PositionMinistry, "Id", "Description", collaborator.IdPositionMinisty);
            return View(collaborator);
        }

        // POST: Collaborator/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IdPositionMinisty,IdPhoneNumber,IdCommonCongregation")] Collaborator collaborator)
        {
            if (id != collaborator.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collaborator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollaboratorExists(collaborator.Id))
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
            ViewData["IdCommonCongregation"] = new SelectList(_context.CommonCongregation, "Id", "Name", collaborator.IdCommonCongregation);
            ViewData["IdPositionMinisty"] = new SelectList(_context.PositionMinistry, "Id", "Description", collaborator.IdPositionMinisty);
            return View(collaborator);
        }

        // GET: Collaborator/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaborator = await _context.Collaborator
                .Include(c => c.IdCommonCongregationNavigation)
                .Include(c => c.IdPositionMinistyNavigation)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (collaborator == null)
            {
                return NotFound();
            }

            return View(collaborator);
        }

        // POST: Collaborator/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collaborator = await _context.Collaborator.SingleOrDefaultAsync(m => m.Id == id);
            _context.Collaborator.Remove(collaborator);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CollaboratorExists(int id)
        {
            return _context.Collaborator.Any(e => e.Id == id);
        }
    }
}
