using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendaCCB.Data.Models;
using AgendaCCB.Web.Models;

namespace AgendaCCB.Web.Controllers
{
    public class CommonCongregationController : Controller
    {
        private readonly agendaccbContext _context;

        public CommonCongregationController(agendaccbContext context)
        {
            _context = context;    
        }

        // GET: CommonCongregation
        public async Task<IActionResult> Index()
        {
            var agendaccbContext = _context.CommonCongregation.Include(c => c.IdCityNavigation);
            return View(await agendaccbContext.ToListAsync());
        }

        // GET: CommonCongregation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commonCongregation = await _context.CommonCongregation
                .Include(c => c.IdCityNavigation)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (commonCongregation == null)
            {
                return NotFound();
            }

            return View(commonCongregation);
        }

        // GET: CommonCongregation/Create
        public IActionResult Create()
        {
            ViewBag.CityList = new SelectList(_context.City, "Id", "Name");
            return View();
        }

        // POST: CommonCongregation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IdCity")] CommonCongregationViewModel commonCongregationVM)
        {
            var commonCongregation = new CommonCongregation();
            if (ModelState.IsValid)
            {
                commonCongregation = MapTo(commonCongregationVM);
                _context.Add(commonCongregation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Id"] = new SelectList(_context.City, "Id", "Name", commonCongregation.Id);
            return View(commonCongregation);
        }

        private CommonCongregation MapTo(CommonCongregationViewModel commonCongregationVM)
        {
            return new CommonCongregation()
            {
                IdCity = commonCongregationVM.IdCity,
                Name = commonCongregationVM.Name
            };
        }

        // GET: CommonCongregation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commonCongregation = await _context.CommonCongregation.SingleOrDefaultAsync(m => m.Id == id);
            if (commonCongregation == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.City, "Id", "Name", commonCongregation.Id);
            return View(commonCongregation);
        }

        // POST: CommonCongregation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IdCity")] CommonCongregation commonCongregation)
        {
            if (id != commonCongregation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commonCongregation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommonCongregationExists(commonCongregation.Id))
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
            ViewData["Id"] = new SelectList(_context.City, "Id", "Name", commonCongregation.Id);
            return View(commonCongregation);
        }

        // GET: CommonCongregation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commonCongregation = await _context.CommonCongregation
                .Include(c => c.IdCityNavigation)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (commonCongregation == null)
            {
                return NotFound();
            }

            return View(commonCongregation);
        }

        // POST: CommonCongregation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commonCongregation = await _context.CommonCongregation.SingleOrDefaultAsync(m => m.Id == id);
            _context.CommonCongregation.Remove(commonCongregation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CommonCongregationExists(int id)
        {
            return _context.CommonCongregation.Any(e => e.Id == id);
        }
    }
}
