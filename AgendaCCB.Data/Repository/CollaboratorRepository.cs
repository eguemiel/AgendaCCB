using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AgendaCCB.Data.Models
{
    public partial class agendaccbContext
    {
        public IQueryable<Collaborator> GetAllColaborators()
        {
            return Collaborator.Include("PhoneNumber");
        }

        public Collaborator GetCollaboratorById(int id)
        {
            return GetAllColaborators().FirstOrDefault(c => c.Id == id);
        }

        public void AddCategoria(Collaborator collaborator)
        {
            Collaborator.Add(collaborator);
        }

        public void DeleteCollaborator(int id)
        {
            var obj = GetCollaboratorById(id);

            foreach (var phoneNumber in obj.PhoneNumber)
            {
                PhoneNumber.Remove(phoneNumber);
            }

            Collaborator.Remove(obj);
        }

        public void EditCollaborator(Collaborator collaborator)
        {
            using (var transaction = Database.BeginTransaction())
            {
                try
                {
                    var oldCollaborator = GetCollaboratorById(collaborator.Id);

                    Collaborator.Update(collaborator);

                    foreach (var phoneNumber in collaborator.PhoneNumber)
                    {
                        if (phoneNumber.Id == new int())
                            PhoneNumber.Add(phoneNumber);
                        else
                            PhoneNumber.Update(phoneNumber);
                    }

                    var removedPhoneNumbers = oldCollaborator.PhoneNumber.Where(p => collaborator.PhoneNumber.Select(pn => pn.Id).Contains(p.Id)).ToList();

                    foreach (var phoneNumbers in removedPhoneNumbers)
                    {
                        PhoneNumber.Remove(phoneNumbers);
                    }


                    transaction.Commit();
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
        }
    }
}
