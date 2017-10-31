using System.Collections.Generic;
using System.ComponentModel;

namespace AgendaCCB.App.Models
{
    public class Collaborator : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CommumCongregation { get; set; }

        public string PositionMinistry { get; set; }

        public string FullDescriptionList { get { return PositionMinistry + " - " + CommumCongregation; } }


        public List<PhoneNumber> PhoneNumber { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class PhoneNumber : INotifyPropertyChanged
    {
        public string Number { get; set; }

        public string PhoneType { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
