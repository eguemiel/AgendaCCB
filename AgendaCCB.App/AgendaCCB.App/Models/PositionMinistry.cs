using System.ComponentModel;

namespace AgendaCCB.App.Models
{
    public class PositionMinistry : INotifyPropertyChanged
    {
        public string Description { get; set; }
        public bool IsMinistry { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}