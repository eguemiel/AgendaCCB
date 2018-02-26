using AgendaCCB.App.Models;
using AgendaCCB.App.ModelsRealm;
using AgendaCCB.App.Services.Api;
using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCCB.App.Services.AppServices
{
    public class CollaboratorService : BaseService
    {
        public void SaveCollaborator(CollaboratorRealm collaborator)
        {
            using (Realm realm = CreateNewRealmInstance())
            {
                var collaboratorRealm = GetCollaborator(realm, collaborator);

                realm.Write(() =>
                {
                    if (collaboratorRealm == null)
                        realm.Add(collaborator);
                    else
                        realm.Add(collaborator, true);
                });
            }
        }        

        private CollaboratorRealm GetCollaborator(Realm realm, CollaboratorRealm collaborator)
        {
            return realm.All<CollaboratorRealm>().FirstOrDefault(c => c.Id == collaborator.Id);
        }

        public async Task<List<Collaborator>> GetAllCollaboratorsFromApi()
        {
            var api = new ApiCollaboratorService();            

            ApiReturn<IList<Collaborator>> objectReturned = await api.GetAllCollaborators();            

            if (objectReturned.Success)
            {
                foreach (var item in objectReturned.Object)
                {
                    CollaboratorRealm sessao = item.ConvertToSession();
                    SaveCollaborator(sessao);
                }               
            }    

            return GetAllCollaboratorsFromRealm().ToList();        
        }

        public IList<Collaborator> GetAllCollaboratorsFromRealm()
        {
            List<Collaborator> collaborators = new List<Collaborator>();
            using (Realm realm = CreateNewRealmInstance())
            {
                var collaboratorsRealm = realm.All<CollaboratorRealm>().ToList();
                collaborators.AddRange(MapToCollaborator(collaboratorsRealm));
            }

            return collaborators;
        }

        private IList<Collaborator> MapToCollaborator(List<CollaboratorRealm> collaborators)
        {
            List<Collaborator> items = new List<Collaborator>();
            foreach (var item in collaborators)
            {
                items.Add(new Collaborator()
                {
                    Id = item.Id,
                    CommumCongregation = item.CommumCongregation,
                    Name = item.Name,
                    PhoneNumber = MapToPhoneNumbers(item.PhoneNumbers.ToList()),
                    PositionMinistry = item.PositionMinistry,
                    PositionMinistryList = MapToPositionMinistry(item.PositionMinistries.ToList())
                });
            }

            return items;
        }

        private List<PositionMinistry> MapToPositionMinistry(IList<PositionMinistryRealm> positionMinistriesRealm)
        {
            List<PositionMinistry> positionMinistries = new List<PositionMinistry>();

            foreach (var item in positionMinistriesRealm)
            {
                positionMinistries.Add(new PositionMinistry()
                {
                    Description = item.Description,
                    IsMinistry = item.IsMinistry
                });
            }
            
            return positionMinistries;
        }

        private List<PhoneNumber> MapToPhoneNumbers(IList<PhoneNumberRealm> phoneNumbersRealm)
        {
            List<PhoneNumber> phoneNumbers = new List<PhoneNumber>();

            foreach (var item in phoneNumbersRealm)
            {
                phoneNumbers.Add(new PhoneNumber()
                {
                    Number = item.Number,
                    PhoneType = item.PhoneType
                });
            }

            return phoneNumbers;
        }
    }
}
