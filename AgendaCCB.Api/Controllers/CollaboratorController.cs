using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AgendaCCB.Api.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AgendaCCB.Api.Controllers
{
    [Route("api/[controller]")]
    public class CollaboratorController : BaseController
    {
        public CollaboratorController(IConfiguration configuration) : base(configuration)
        {
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var collaboratorsBD = _context.Collaborator
                                          .Include("PhoneNumber")
                                          .Include("IdCommonCongregationNavigation")
                                          .Include("IdPositionMinistyNavigation").ToList();

            var collaborators = await MapTo(collaboratorsBD);
            return Ok(collaborators);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Collaborator Get(int id)
        {
            var collaborator = MapTo(_context.Collaborator.ToList().Find(c => c.Id == id));
            return collaborator;
        }

        private async Task<List<Collaborator>> MapTo(List<Data.Models.Collaborator> collaboratorsBD)
        {
            List<Collaborator> collaborators = new List<Collaborator>();

            foreach (var collaborator in collaboratorsBD)
            {
                collaborators.Add(MapTo(collaborator));
            }

            return await Task.FromResult(collaborators);
        }

        private Collaborator MapTo(Data.Models.Collaborator collaboratorBD)
        {
            var phoneNumbers = new List<PhoneNumber>();

            foreach (var phone in collaboratorBD.PhoneNumber)
            {
                phoneNumbers.Add(new PhoneNumber
                {
                    Number = phone.Number,
                    PhoneType = phone.Type.ToString()
                });
            }
            var collaborator = new Collaborator()
            {
                Id = collaboratorBD.Id,
                CommumCongregation = collaboratorBD.IdCommonCongregationNavigation.Name,
                Name = collaboratorBD.Name,
                PositionMinistry = collaboratorBD.IdPositionMinistyNavigation.Description,
                PhoneNumber = phoneNumbers
            };

            return collaborator;
        }
    }
}
