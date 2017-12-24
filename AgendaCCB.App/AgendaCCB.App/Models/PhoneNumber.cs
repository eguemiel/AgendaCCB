using System.ComponentModel;

namespace AgendaCCB.App.Models
{
    public class PhoneNumber : INotifyPropertyChanged
    {
        public string Number { get; set; }

        public string PhoneType { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
