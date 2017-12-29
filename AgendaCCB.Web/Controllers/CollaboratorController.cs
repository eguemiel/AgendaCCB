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
            var collaborators = _context.GetAllColaborators();

            return View(await collaborators.ToListAsync());
        }

        // GET: Collaborator/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaborator = _context.GetCollaboratorById(id);
                
            if (collaborator == null)
            {
                return NotFound();
            }

            return View(await collaborator);
        }

        // GET: Collaborator/Create
        public IActionResult Create()
        {
            ViewBag.CommonList = _context.CommonCongregation.Select(p => new SelectListItem
            {
                Text = p.Name + "-" + p.CityNavigation.Name,
                Value = p.Id.ToString()
            });
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

            ViewBag.CommonList = _context.CommonCongregation.Select(p => new SelectListItem
            {
                Text = p.Name + "-" + p.CityNavigation.Name,
                Value = p.Id.ToString(),
                Selected = p.Id == collaboratorVM.IdCommonCongregation                
            });
            
            ViewBag.PositionMinistryList = new SelectList(_context.PositionMinistry, "Id", "Description", collaboratorVM.IdPositionMinistry);
            ViewBag.TypePhoneList = EnumHelper<TypePhone>.GetSelectListEnum();

            return View(collaboratorVM);
        }

        // GET: Collaborator/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaborator = await _context.GetCollaboratorById(id);
            if (collaborator == null)
            {
                return NotFound();
            }

            var collaboratorVM = MapTo(collaborator);

            ViewBag.CommonList = _context.CommonCongregation.Select(p => new SelectListItem
            {
                Text = p.Name + "-" + p.CityNavigation.Name,
                Value = p.Id.ToString(),
                Selected = p.Id == collaboratorVM.IdCommonCongregation
            });

            ViewBag.PositionMinistryList = new SelectList(_context.PositionMinistry, "Id", "Description", collaboratorVM.IdPositionMinistry);
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

                    var oldPositionMinistryObj = _context.PositionMinistryCollaborator.AsNoTracking()
                        .Where(pmc => pmc.IdCollaborator == collaborator.Id && !collaborator.PositionMinistryCollaborator.Select(p => p.Id).Contains(pmc.Id));

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

                    foreach (var positionMinistry in collaborator.PositionMinistryCollaborator)
                    {
                        if (positionMinistry.Id == new int())
                            _context.PositionMinistryCollaborator.Add(positionMinistry);
                        else
                            _context.PositionMinistryCollaborator.Update(positionMinistry);
                    }

                    foreach (var oldPositionMinistry in oldPositionMinistryObj)
                    {
                        _context.PositionMinistryCollaborator.Remove(oldPositionMinistry);
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
            ViewBag.CommonList = _context.CommonCongregation.Select(p => new SelectListItem
            {
                Text = p.Name + "-" + p.CityNavigation.Name,
                Value = p.Id.ToString(),
                Selected = p.Id == collaboratorVM.IdCommonCongregation
            });

            ViewData["IdPositionMinistry"] = new SelectList(_context.PositionMinistry, "Id", "Description", collaboratorVM.IdPositionMinistry);
            return View(collaboratorVM);
        }

        // GET: Collaborator/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaborator = _context.GetCollaboratorById(id);

            if (collaborator == null)
            {
                return NotFound();
            }

            return View(await collaborator);
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
            collaborator.IdCommonCongregation = collaboratorVM.IdPositionMinistry;
            collaborator.Name = collaboratorVM.Name;
            collaborator.Id = collaboratorVM.Id;

            collaborator.PhoneNumber = new List<PhoneNumber>();
            collaborator.PositionMinistryCollaborator = new List<PositionMinistryCollaborator>();

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

            foreach (var positionMinistry in collaboratorVM.PositionMinistryList)
            {
                collaborator.PositionMinistryCollaborator.Add(new PositionMinistryCollaborator()
                {
                    IdCollaborator = collaboratorVM.Id,
                    IdPositionMinistry = positionMinistry.Id
                });
            }

            return collaborator;
        }

        private CollaboratorViewModel MapTo(Collaborator collaborator)
        {
            CollaboratorViewModel collaboratorVM = new CollaboratorViewModel();

            collaboratorVM.IdCommonCongregation = collaborator.IdCommonCongregation;
            collaboratorVM.IdPositionMinistry = collaborator.IdCommonCongregation;
            collaboratorVM.Name = collaborator.Name;

            collaborator.PhoneNumber = _context.PhoneNumber.Where(p => p.IdCollaborador == collaborator.Id).ToList();

            collaboratorVM.PhoneNumberList = new List<PhoneNumberViewModel>();
            collaboratorVM.PositionMinistryList = new List<PositionMinistryViewModel>();

            foreach (var phone in collaborator.PhoneNumber)
            {
                var phoneViewModel = new PhoneNumberViewModel();
                phoneViewModel.Id = phone.Id;
                phoneViewModel.Phone = phone.Number;
                phoneViewModel.TypePhone = (TypePhone)Enum.Parse(typeof(TypePhone), phone.Type);

                collaboratorVM.PhoneNumberList.Add(phoneViewModel);                
            }

            foreach (var positionMinistry in collaborator.PositionMinistryCollaborator)
            {
                var positionMinistryVM = new PositionMinistryViewModel();
                positionMinistryVM.Id = positionMinistry.IdPositionMinistry;
                positionMinistryVM.Description = _context.PositionMinistry.SingleOrDefault(pm => pm.Id == positionMinistry.IdPositionMinistry).Description;
                collaboratorVM.PositionMinistryList.Add(positionMinistryVM);
            }

            return collaboratorVM;
        }
    }
}
