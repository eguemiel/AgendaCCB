using Realms;
using System;
using System.ComponentModel;

namespace AgendaCCB.App.ModelsRealm
{
    public class PositionMinistryRealm : RealmObject
    {
        [PrimaryKey]
        public string RealmId { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; }
        public bool IsMinistry { get; set; }
        public CollaboratorRealm CollaboratorRealm { get; set; }
    }
}