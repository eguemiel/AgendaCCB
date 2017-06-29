using AgendaCCB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCCB
{
    public class ContactRepository : IContactRepository
    {
        static List<Contact> ContactList = new List<Contact>();

        public void Add(Contact item)
        {
            ContactList.Add(item);
        }

        public void Delete(string Id)
        {
            var itemToDelete = ContactList.Where(c => c.PhoneNumber == Id).FirstOrDefault();
            ContactList?.Remove(itemToDelete);
        }

        public Contact Find(string key)
        {
            return ContactList.Where(e => e.PhoneNumber.Equals(key)).FirstOrDefault();
        }

        public IEnumerable<Contact> GetAll()
        {
            return ContactList;
        }

        public void Update(Contact item)
        {
            var itemAtualizar = ContactList.SingleOrDefault(r => r.PhoneNumber == item.PhoneNumber);
            if (itemAtualizar != null)
            {
                itemAtualizar.Name = item.Name;
                itemAtualizar.SurName = item.SurName;
                itemAtualizar.IsParente = item.IsParente;
                itemAtualizar.Company = item.Company;
                itemAtualizar.Email = item.Email;
                itemAtualizar.PhoneNumber = item.PhoneNumber;
                itemAtualizar.BornDate = item.BornDate;
            }

        }
    }
}
