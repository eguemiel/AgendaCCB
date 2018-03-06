using AgendaCCB.App.ModelsRealm;
using System.ComponentModel;

namespace AgendaCCB.App.Models
{
    public class UsefulPhone : INotifyPropertyChanged
    {
        public string LocalName { get; set; }

        public string PhoneNumber { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void MapTo(UsefulPhoneRealm usefulPhoneRealm)
        {
            usefulPhoneRealm.LocalName = LocalName;
            usefulPhoneRealm.PhoneNumber = PhoneNumber;
        }

        public UsefulPhoneRealm ConvertToSession()
        {
            var session = new UsefulPhoneRealm();
            MapTo(session);
            return session;
        }
    }
}
