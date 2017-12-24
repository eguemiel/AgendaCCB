using System.Collections.Generic;
using System.ComponentModel;

namespace AgendaCCB.App.Models
{
    public class Collaborator : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CommumCongregation { get; set; }

        public string FullDescriptionList { get { return CommumCongregation; } }

        public List<PhoneNumber> PhoneNumber { get; set; }

        public List<PositionMinistry> PositionMinistry { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }    
}
