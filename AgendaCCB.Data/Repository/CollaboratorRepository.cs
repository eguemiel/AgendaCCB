using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCCB.Data.Models
{
    public partial class agendaccbContext
    {
        public IQueryable<Collaborator> GetAllColaborators()
        {
            var collaborators = Collaborator
                                    .Include(c => c.PhoneNumber)
                                    .Include("PositionMinistryCollaborator.IdPositionMinistryNavigation")
                                    .Include(c => c.IdCommonCongregationNavigation);          

            return collaborators;
        }

        public async Task<Collaborator> GetCollaboratorById(int id)
        {
            return await GetAllColaborators().SingleOrDefaultAsync(c => c.Id == id);
        }


        public async void DeleteCollaborator(int id)
        {
            var obj = await GetCollaboratorById(id);

            foreach (var phoneNumber in obj.PhoneNumber)
            {
                PhoneNumber.Remove(phoneNumber);
            }

            Collaborator.Remove(obj);
        }

        public async void EditCollaborator(Collaborator collaborator)
        {
            using (var transaction = Database.BeginTransaction())
            {
                try
                {
                    var oldCollaborator = await GetCollaboratorById(collaborator.Id);

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
