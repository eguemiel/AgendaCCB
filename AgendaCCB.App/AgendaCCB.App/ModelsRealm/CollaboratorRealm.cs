using Realms;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AgendaCCB.App.ModelsRealm
{
    public class CollaboratorRealm : RealmObject
    {
        [PrimaryKey]
        public string RealmId { get; set; } = Guid.NewGuid().ToString();
        public long Id { get; set; }
        public string Name { get; set; }
        public string CommumCongregation { get; set; }
        public string PositionMinistry { get; set; }
        public IList<PhoneNumberRealm> PhoneNumbers { get; }
        public IList<PositionMinistryRealm> PositionMinistries { get; }          
    }    
}
