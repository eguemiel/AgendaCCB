using AgendaCCB.App.ModelsRealm;
using System.Collections.Generic;
using System.ComponentModel;

namespace AgendaCCB.App.Models
{
    public class Collaborator : INotifyPropertyChanged
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string CommumCongregation { get; set; }

        public string FullDescriptionList { get { return CommumCongregation; } }

        public string PositionMinistry { get; set; }

        public List<PhoneNumber> PhoneNumber { get; set; }

        public List<PositionMinistry> PositionMinistryList { get; set; }        

        public event PropertyChangedEventHandler PropertyChanged;

        public void MapTo(CollaboratorRealm collaboratorRealm)
        {
            collaboratorRealm.Id = Id;
            ((List<PhoneNumberRealm>)collaboratorRealm.PhoneNumbers).AddRange(MapTo(PhoneNumber));
            ((List<PositionMinistryRealm>)collaboratorRealm.PositionMinistries).AddRange(MapTo(PositionMinistryList));
            collaboratorRealm.CommumCongregation = CommumCongregation;
            collaboratorRealm.Name = Name;               
        }

        public CollaboratorRealm ConvertToSession()
        {
            var session = new CollaboratorRealm();
            MapTo(session);
            return session;
        }

        public List<PhoneNumberRealm> MapTo(List<PhoneNumber> phoneNumber)
        {
            List<PhoneNumberRealm> returnObject = new List<PhoneNumberRealm>();

            foreach (var item in phoneNumber)
            {
                returnObject.Add(new PhoneNumberRealm
                {
                    Number = item.Number,
                    PhoneType = item.PhoneType
                });
            }

            return returnObject;
        }

        public List<PositionMinistryRealm> MapTo(List<PositionMinistry> positionMinistry)
        {
            List<PositionMinistryRealm> returnObject = new List<PositionMinistryRealm>();

            foreach (var item in positionMinistry)
            {
                returnObject.Add(new PositionMinistryRealm
                {
                    Description = item.Description,
                    IsMinistry = item.IsMinistry
                });
            }

            return returnObject;
        }
    }    
}
