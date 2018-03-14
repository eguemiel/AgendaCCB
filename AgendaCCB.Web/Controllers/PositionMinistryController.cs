using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaCCB.Data.Models;
using AgendaCCB.Web.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace AgendaCCB.Web.Controllers
{
    public class PositionMinistryController : BaseController
    {
        public PositionMinistryController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {
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
        public async Task<IActionResult> Create(PositionMinistryViewModel positionMinistryVM)
        {
            if (ModelState.IsValid)
            {
                var positionMinistry = this.MapTo(positionMinistryVM);
                _context.Add(positionMinistry);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(positionMinistryVM);
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

            var positionMinistryVM = MapTo(positionMinistry);
            return View(positionMinistryVM);
        }

        // POST: PositionMinistry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PositionMinistryViewModel positionMinistryVM)
        {
            if (id != positionMinistryVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var positionMinistry = MapTo(positionMinistryVM);
                    _context.Update(positionMinistry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PositionMinistryExists(positionMinistryVM.Id))
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
            return View(positionMinistryVM);
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

        private PositionMinistry MapTo(PositionMinistryViewModel positionMinistryVM)
        {
            var positionMinistry = new PositionMinistry();
            positionMinistry.Description = positionMinistryVM.Description;
            positionMinistry.IsMinistry = positionMinistryVM.IsMinistry;
            positionMinistry.Id = positionMinistryVM.Id;
            return positionMinistry;
        }

        private PositionMinistryViewModel MapTo(PositionMinistry positionMinistry)
        {
            var positionMinistryVM = new PositionMinistryViewModel();
            positionMinistryVM.Description = positionMinistry.Description;
            positionMinistryVM.IsMinistry = positionMinistry.IsMinistry;
            return positionMinistryVM;
        }
    }
}
