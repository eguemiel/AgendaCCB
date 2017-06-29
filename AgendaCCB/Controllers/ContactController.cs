using AgendaCCB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AgendaCCB.Controllers
{
    [Route("api/[controller]")]
    public class ContactController: Controller
    {
        public IContactRepository ContactsRepo { get; set; }

        public ContactController(IContactRepository _repo)
        {
            ContactsRepo = _repo;
        }

        [HttpGet]
        public IEnumerable<Contact> GetAll()
        {
            return ContactsRepo.GetAll();
        }

        [HttpGet("{id}", Name = "GetContacts")]
        public IActionResult GetPorId(string id)
        {
            var item = ContactsRepo.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Contact item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            ContactsRepo.Add(item);
            return CreatedAtRoute("GetContacts", new { Controller = "Contacts", id = item.PhoneNumber }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(string id, [FromBody] Contact item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            var contactObj = ContactsRepo.Find(id);
            if (contactObj == null)
            {
                return NotFound();
            }
            ContactsRepo.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public void Deletar(string id)
        {
            ContactsRepo.Delete(id);
        }
    }

    }
}
