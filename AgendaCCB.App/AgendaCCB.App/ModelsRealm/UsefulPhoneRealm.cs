using Realms;
using System.ComponentModel;

namespace AgendaCCB.App.ModelsRealm
{
    public class UsefulPhoneRealm : RealmObject
    {
        [PrimaryKey]
        public long RealmId { get; set; }
        public string LocalName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
