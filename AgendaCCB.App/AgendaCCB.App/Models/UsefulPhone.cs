using System.ComponentModel;

namespace AgendaCCB.App.Models
{
    public class UsefulPhone : INotifyPropertyChanged
    {
        public string LocalName { get; set; }

        public string PhoneNumber { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
