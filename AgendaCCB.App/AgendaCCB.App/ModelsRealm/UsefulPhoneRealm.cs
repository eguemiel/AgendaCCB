using Realms;
using System;

namespace AgendaCCB.App.ModelsRealm
{
    public class UsefulPhoneRealm : RealmObject
    {
        [PrimaryKey]
        public string RealmId { get; set; } = Guid.NewGuid().ToString();
        public string LocalName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
