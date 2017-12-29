using AgendaCCB.App.Models;
using Prism.Navigation;
using PropertyChanged;
using System;
using Xamarin.Forms;

namespace AgendaCCB.App.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class CollaboratorPageViewModel : BaseViewModel
    {
        public Collaborator Collaborator { get; set; }

        public CollaboratorPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Detalhes Colaborador";            
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            Collaborator = (Collaborator)parameters["Collaborator"] != null ? (Collaborator)parameters["Collaborator"] : Collaborator;
        }
    }
}
