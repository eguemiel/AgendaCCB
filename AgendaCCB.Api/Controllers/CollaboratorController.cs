using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AgendaCCB.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AgendaCCB.Api.Authorization;
using System.Net.Http;
using System.Net;

namespace AgendaCCB.Api.Controllers
{
    [Route("api/[controller]")]
    public class CollaboratorController : BaseController
    {
        public CollaboratorController(IConfiguration configuration) : base(configuration)
        {
        }

        // GET api/values
        [BasicAuthentication]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var collaboratorsBD = _context.Collaborator
                                              .Include("PhoneNumber")
                                              .Include("IdCommonCongregationNavigation")
                                              .Include("IdPositionMinistyNavigation").ToList();

                var collaborators = MapTo(collaboratorsBD);

                var apiRetorno = new ApiReturn(){Status=(int)HttpStatusCode.OK, Object=collaborators };

                return new ObjectResult(apiRetorno);
            }
            catch (System.Exception ex)
            {
                var apiRetorno = new ApiReturn() { Status = (int)HttpStatusCode.BadRequest, Message = ex.Message };
                return new ObjectResult(apiRetorno);
            }
        }

        // GET api/values/5
        [BasicAuthentication]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var collaborator = MapTo(_context.Collaborator.ToList().Find(c => c.Id == id));

                HttpStatusCode statusCode = collaborator == null ? HttpStatusCode.NotFound : HttpStatusCode.OK;

                var apiRetorno = new ApiReturn { Status = (int)statusCode, Object = collaborator };

                return new ObjectResult(apiRetorno);
            }
            catch (System.Exception ex)
            {
                var apiRetorno = new ApiReturn { Status = (int)HttpStatusCode.BadRequest, Message = ex.Message };
                return new ObjectResult(apiRetorno);
            }
        }

        private List<Collaborator> MapTo(List<Data.Models.Collaborator> collaboratorsBD)
        {
            List<Collaborator> collaborators = new List<Collaborator>();

            foreach (var collaborator in collaboratorsBD)
            {
                collaborators.Add(MapTo(collaborator));
            }

            return collaborators;
        }

        private Collaborator MapTo(Data.Models.Collaborator collaboratorBD)
        {
            if (collaboratorBD == null)
                return null;

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
