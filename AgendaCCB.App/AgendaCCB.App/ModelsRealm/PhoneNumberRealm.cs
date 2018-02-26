using Realms;
using System;
using System.ComponentModel;

namespace AgendaCCB.App.ModelsRealm
{
    public class PhoneNumberRealm : RealmObject
    {
        [PrimaryKey]
        public string RealmId { get; set; } = Guid.NewGuid().ToString();
        public int IdCollaborator { get; set; }
        public string Number { get; set; }
        public string PhoneType { get; set; }

        public CollaboratorRealm CollaboratorRealm { get; set; }
    }
}
