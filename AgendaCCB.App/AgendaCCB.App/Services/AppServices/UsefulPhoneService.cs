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
    public class UsefulPhoneService : BaseService
    {
        public void SaveUsefulPhone(UsefulPhoneRealm usefulPhone)
        {
            using (Realm realm = CreateNewRealmInstance())
            {
                realm.Write(() => realm.Add(usefulPhone));
            }
        }

        public void CleanUsefulPhones()
        {
            using (Realm realm = CreateNewRealmInstance())
            {
                realm.Write(() => realm.RemoveAll<UsefulPhoneRealm>());
            }
        }

        public async Task<List<UsefulPhone>> GetAllUsefulPhonesFromApi()
        {
            var api = new ApiUsefulPhoneService();            

            ApiReturn<IList<UsefulPhone>> objectReturned = await api.GetAllUsefulPhones();            

            if (objectReturned.Success)
            {
                foreach (var item in objectReturned.Object)
                {
                    UsefulPhoneRealm sessao = item.ConvertToSession();
                    SaveUsefulPhone(sessao);
                }               
            }    

            return GetAllUsefulPhonesFromRealm().ToList();        
        }

        public IList<UsefulPhone> GetAllUsefulPhonesFromRealm()
        {
            List<UsefulPhone> phones = new List<UsefulPhone>();
            using (Realm realm = CreateNewRealmInstance())
            {
                var phonesRealm = realm.All<UsefulPhoneRealm>().ToList();
                phones.AddRange(MapToUsefulPhone(phonesRealm));
            }

            return phones;
        }

        private IList<UsefulPhone> MapToUsefulPhone(List<UsefulPhoneRealm> phonesCongregation)
        {
            List<UsefulPhone> items = new List<UsefulPhone>();
            foreach (var item in phonesCongregation)
            {
                items.Add(new UsefulPhone()
                {
                    LocalName = item.LocalName,
                    PhoneNumber = item.PhoneNumber
                });
            }

            return items;
        }       
    }
}
