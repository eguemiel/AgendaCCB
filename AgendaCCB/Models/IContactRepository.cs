using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCCB.Models
{
    public interface IContactRepository
    {
        void Add(Contact item);
        IEnumerable<Contact> GetAll();
        Contact Find(string key);
        void Delete(string Id);
        void Update(Contact item);

    }
}
