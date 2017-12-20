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

namespace AgendaCCB.Web.Controllers
{
    public class CollaboratorController : BaseController
    {
        public CollaboratorController(IConfiguration configuration) : base(configuration)
        {
        }
        
        // GET: Collaborator
        public async Task<IActionResult> Index()
        {
            var query = from collaborator in _context.Collaborator
                        join pmc in _context.PositionMinistryCollaborator on collaborator.Id equals pmc.IdCollaborator
                        join pm in _context.PositionMinistry on pmc.IdPositionMinistry equals pm.Id
                        select new
                        {
                            IdCollaborator = collaborator.Id,
                            Name = collaborator.Name,
                            IdPositionMinistry = pm.Id,
                            IsMinistry = pm.IsMinistry,
                            DescriptionMinistry = pm.Description
                        };

            return View(await query.ToListAsync());
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
                .Include(c => c.IdCommonCongregation)
                .Include("PhoneNumber")
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
            ViewBag.TypePhoneList = EnumHelper<TypePhone>.GetSelectListEnum();

            return View();
        }

        // POST: Collaborator/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CollaboratorViewModel collaboratorVM)
        {
            if (ModelState.IsValid)
            {
                Collaborator collaborator = this.MapTo(collaboratorVM);
                _context.Add(collaborator);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CommonList = new SelectList(_context.CommonCongregation, "Id", "Name", collaboratorVM.IdCommonCongregation);
            ViewBag.PositionMinistryList = new SelectList(_context.PositionMinistry, "Id", "Description", collaboratorVM.IdPositionMinisty);
            ViewBag.TypePhoneList = EnumHelper<TypePhone>.GetSelectListEnum();

            return View(collaboratorVM);
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

            var collaboratorVM = MapTo(collaborator);

            ViewBag.CommonList = new SelectList(_context.CommonCongregation, "Id", "Name", collaboratorVM.IdCommonCongregation);
            ViewBag.PositionMinistryList = new SelectList(_context.PositionMinistry, "Id", "Description", collaboratorVM.IdPositionMinisty);
            ViewBag.TypePhoneList = EnumHelper<TypePhone>.GetSelectListEnum();
            
            return View(collaboratorVM);
        }

        // POST: Collaborator/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CollaboratorViewModel collaboratorVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var collaborator = MapTo(collaboratorVM);

                    var oldPhoneNumbersObj = _context.PhoneNumber.AsNoTracking()
                        .Where(pn => pn.IdCollaborador == collaborator.Id && !collaborator.PhoneNumber.Select(p => p.Id).Contains(pn.Id));

                    foreach (var phoneNumber in collaborator.PhoneNumber)
                    {
                        if (phoneNumber.Id == new int())
                            _context.PhoneNumber.Add(phoneNumber);
                        else
                            _context.PhoneNumber.Update(phoneNumber);
                    }

                    foreach (var oldPhoneNumber in oldPhoneNumbersObj)
                    {
                        _context.PhoneNumber.Remove(oldPhoneNumber);
                    }

                    _context.Update(collaborator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollaboratorExists(collaboratorVM.Id))
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
            ViewData["IdCommonCongregation"] = new SelectList(_context.CommonCongregation, "Id", "Name", collaboratorVM.IdCommonCongregation);
            ViewData["IdPositionMinisty"] = new SelectList(_context.PositionMinistry, "Id", "Description", collaboratorVM.IdPositionMinisty);
            return View(collaboratorVM);
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
                .Include(c => c.IdCommonCongregation)
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

        private Collaborator MapTo(CollaboratorViewModel collaboratorVM)
        {
            Collaborator collaborator = new Collaborator();

            collaborator.IdCommonCongregation = collaboratorVM.IdCommonCongregation;
            collaborator.IdCommonCongregation = collaboratorVM.IdPositionMinisty;
            collaborator.Name = collaboratorVM.Name;
            collaborator.Id = collaboratorVM.Id;

            collaborator.PhoneNumber = new List<PhoneNumber>();

            foreach (var phone in collaboratorVM.PhoneNumberList)
            {
                collaborator.PhoneNumber.Add(new PhoneNumber()
                {
                    Number = phone.Phone,
                    Type = phone.TypePhone.ToString(),
                    IdCollaborador = collaboratorVM.Id,
                    Id = phone.Id                    
                });
            }

            return collaborator;
        }

        private CollaboratorViewModel MapTo(Collaborator collaborator)
        {
            CollaboratorViewModel collaboratorVM = new CollaboratorViewModel();

            collaboratorVM.IdCommonCongregation = collaborator.IdCommonCongregation;
            collaboratorVM.IdPositionMinisty = collaborator.IdCommonCongregation;
            collaboratorVM.Name = collaborator.Name;

            collaborator.PhoneNumber = _context.PhoneNumber.Where(p => p.IdCollaborador == collaborator.Id).ToList();

            collaboratorVM.PhoneNumberList = new List<PhoneNumberViewModel>();

            foreach (var phone in collaborator.PhoneNumber)
            {
                var phoneViewModel = new PhoneNumberViewModel();
                phoneViewModel.Id = phone.Id;
                phoneViewModel.Phone = phone.Number;
                phoneViewModel.TypePhone = (TypePhone)Enum.Parse(typeof(TypePhone), phone.Type);

                collaboratorVM.PhoneNumberList.Add(phoneViewModel);
                
            }

            return collaboratorVM;
        }
    }
}
