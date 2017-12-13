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
using AgendaCCB.App.Services.Api;

namespace AgendaCCB.App.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPrincipalViewModel : BaseViewModel
    {
        public ObservableCollection<Collaborator> Collaborators { get; set; }

        public Command<Collaborator> ShowCollaboratorCommand { get; }

        public Command ItemTappedCommand { get; set; }

        public MainPrincipalViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Agenda CCB" + DateTime.Now.Year;
            ShowCollaboratorCommand = new Command<Collaborator>(ExecuteShowCategoriaCommand);
            Task.Run(async () =>
            {   
                Collaborators = new ObservableCollection<Collaborator>(await LoadCollaborators());
                RaisePropertyChanged(nameof(Collaborators));
            });

            this.ItemTappedCommand = new Command(async item =>
            {
                var collaboratorItem = (Collaborator)item;
                var navigationParams = new NavigationParameters
                {
                    { "Collaborator", collaboratorItem }
                };

                CanNavigate = false;
                await _navigationService.NavigateAsync("CollaboratorPage", navigationParams);
                CanNavigate = true;
            }, canExecute: (x) => CanNavigate);

        }

        private async void ExecuteShowCategoriaCommand(Collaborator collaborator)
        {
            await _navigationService.NavigateAsync("Collaborator", new NavigationParameters(collaborator.Id.ToString()));
        }

        private async Task<IList<Collaborator>> LoadCollaborators()
        {
            IList<Collaborator> collaborators = new List<Collaborator>();

            var api = new ApiColaboratorService();

            collaborators = await api.GetAllCollaborators();

            return collaborators;
        }
    }
}
