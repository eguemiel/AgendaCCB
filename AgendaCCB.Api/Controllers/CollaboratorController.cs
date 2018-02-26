﻿using System.Collections.Generic;
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
                var collaboratorsBD = _context.GetAllColaborators().ToList();

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

            foreach (var collaborator in collaboratorsBD.OrderBy(c => c.Name))
            {
                collaborators.AddRange(MapTo(collaborator));
            }

            return collaborators;
        }

        private List<Collaborator> MapTo(Data.Models.Collaborator collaboratorBD)
        {
            if (collaboratorBD == null)
                return null;

            List<Collaborator> collaborator = new List<Collaborator>();

            foreach (var positionMinistry in collaboratorBD.PositionMinistryCollaborator)
            {
                var phoneNumbers = new List<PhoneNumber>();
                var positionMinistryList = new List<PositionMinistry>();

                foreach (var phone in collaboratorBD.PhoneNumber)
                {
                    phoneNumbers.Add(new PhoneNumber
                    {
                        Number = phone.Number,
                        PhoneType = EnumHelper<TypePhone>.GetDisplayValue(EnumHelper<TypePhone>.Parse(phone.Type))
                    });
                }

                foreach (var position in collaboratorBD.PositionMinistryCollaborator)
                {
                    positionMinistryList.Add(new PositionMinistry{
                        Description = position.IdPositionMinistryNavigation.Description,
                        IsMinistry = position.IdPositionMinistryNavigation.IsMinistry
                    });
                }

                collaborator.Add(new Collaborator()
                {
                    Id = collaboratorBD.Id,
                    CommumCongregation = collaboratorBD.IdCommonCongregationNavigation.Name,
                    Name = collaboratorBD.Name,
                    PhoneNumber = phoneNumbers,
                    PositionMinistryList = positionMinistryList
                });
            }
            return collaborator;
        }
    }
}
