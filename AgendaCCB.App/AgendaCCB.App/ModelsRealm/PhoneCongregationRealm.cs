using Realms;
using System.ComponentModel;

namespace AgendaCCB.App.ModelsRealm
{
    public class PhoneCongregationRealm : RealmObject
    {
        [PrimaryKey]
        public long RealmId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxPhoneNumber { get; set; }
    }
}
