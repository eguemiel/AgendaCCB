using System.ComponentModel;

namespace AgendaCCB.App.Models
{
    public class PhoneCongregation : INotifyPropertyChanged
    {
        public string Name { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string District { get; set; }

        public string ZipCode { get; set; }

        public string PhoneNumber { get; set; }

        public string FaxPhoneNumber { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
