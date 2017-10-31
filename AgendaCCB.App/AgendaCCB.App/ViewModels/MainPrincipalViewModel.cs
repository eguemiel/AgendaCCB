using AgendaCCB.App.Models;
using AgendaCCB.App.Services;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System;
using Prism.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;
using PropertyChanged;
using Acr.UserDialogs;
using AgendaCCB.App.Helpers;
using AgendaCCB.App.Controls;

namespace AgendaCCB.App.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPrincipalViewModel : BaseViewModel
    {
        public ObservableCollection<Collaborator> Collaborators { get; set; }

        public Command<Collaborator> ShowCollaboratorCommand { get; }

        public MainPrincipalViewModel(INavigationService navigationService) : base(navigationService)
        {

            ShowCollaboratorCommand = new Command<Collaborator>(ExecuteShowCategoriaCommand);
            Task.Run(async () =>
            {

                Collaborators = new ObservableCollection<Collaborator>(await LoadCollaborators());
                RaisePropertyChanged(nameof(Collaborators));
            });

        }

        private async void ExecuteShowCategoriaCommand(Collaborator collaborator)
        {
            await _navigationService.NavigateAsync("Collaborator", new NavigationParameters(collaborator.Id.ToString()));
        }

        private async Task<IList<Collaborator>> LoadCollaborators()
        {
            IList<Collaborator> collaborators = new List<Collaborator>();

            collaborators = await _agendaCCBApiService.GetAllCollaborators();

            return collaborators;
        }
    }
}
