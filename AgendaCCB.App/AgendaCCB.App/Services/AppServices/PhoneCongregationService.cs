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
    public class PhoneCongregationService : BaseService
    {
        public void SavePhoneCongregation(PhoneCongregationRealm phoneCongregation)
        {
            using (Realm realm = CreateNewRealmInstance())
            {
                realm.Write(() => realm.Add(phoneCongregation));
            }
        }

        public void CleanPhoneCongregations()
        {
            using (Realm realm = CreateNewRealmInstance())
            {
                realm.Write(() => realm.RemoveAll<PhoneCongregationRealm>());
            }
        }
       
        public async Task<List<PhoneCongregation>> GetAllPhonesFromApi()
        {
            var api = new ApiPhoneCongregationService();            

            ApiReturn<IList<PhoneCongregation>> objectReturned = await api.GetAllPhoneCongregations();            

            if (objectReturned.Success)
            {
                foreach (var item in objectReturned.Object)
                {
                    PhoneCongregationRealm sessao = item.ConvertToSession();
                    SavePhoneCongregation(sessao);
                }               
            }    

            return GetAllPhonesFromRealm().ToList();        
        }

        public IList<PhoneCongregation> GetAllPhonesFromRealm()
        {
            List<PhoneCongregation> phones = new List<PhoneCongregation>();
            using (Realm realm = CreateNewRealmInstance())
            {
                var phonesRealm = realm.All<PhoneCongregationRealm>().ToList();
                phones.AddRange(MapToPhoneCongregation(phonesRealm));
            }

            return phones;
        }

        private IList<PhoneCongregation> MapToPhoneCongregation(List<PhoneCongregationRealm> phonesCongregation)
        {
            List<PhoneCongregation> items = new List<PhoneCongregation>();
            foreach (var item in phonesCongregation)
            {
                items.Add(new PhoneCongregation()
                {
                    Address = item.Address,
                    City = item.City,
                    District = item.District,
                    FaxPhoneNumber = item.FaxPhoneNumber,
                    Name = item.Name,
                    PhoneNumber = item.PhoneNumber,
                    ZipCode = item.ZipCode
                });
            }

            return items;
        }       
    }
}
