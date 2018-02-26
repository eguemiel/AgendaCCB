using AgendaCCB.App.Models;
using Xamarin.Forms;
using System;
using Prism.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;
using PropertyChanged;
using Acr.UserDialogs;
using AgendaCCB.App.Helpers;
using AgendaCCB.App.Services.Api;
using System.Linq;
using AgendaCCB.App.Controls;
using AgendaCCB.App.Services.AppServices;
using AgendaCCB.App.ModelsRealm;

namespace AgendaCCB.App.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPrincipalViewModel : BaseViewModel
    {
        private string _searchText = string.Empty;

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText != value)
                {
                    _searchText = value ?? string.Empty;
                    RaisePropertyChanged(nameof(SearchText));
                    // Perform the search
                    if (SearchCommand.CanExecute(null))
                    {
                        SearchCommand.Execute(null);
                    }
                }
            }
        }
        public bool IsRefreshing { get; set; }
        public List<Collaborator> Collaborators { get; set; }
        public IEnumerable<Group<string, Collaborator>> CollaboratorsList { get; set; }
        public List<UsefulPhone> UsefulPhones { get; set; }
        public List<PhoneCongregation> PhoneCongregations { get; set; }
        public Command<Collaborator> ShowCollaboratorCommand { get; }
        public Command ItemTappedCommand { get; set; }
        public Command UsefulListTappedCommand { get; set; }
        public Command PhoneCongregationListTappedCommand { get; set; }
        public Command SearchCommand { get; set; }
        public Command RefreshCollaboratorCommand { get; set; }

        public MainPrincipalViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Agenda CCB " + DateTime.Now.Year;
            ShowCollaboratorCommand = new Command<Collaborator>(ExecuteShowCategoriaCommand);
            RefreshCollaboratorCommand = new Command(ExecuteRefreshCollaboratorCommand);
            Task.Run(async () =>
            {
                UserDialogs.Instance.ShowLoading(AppSettings.WaitingText, MaskType.Black);
                Collaborators = new List<Collaborator>(await LoadCollaborators());
                UsefulPhones = new List<UsefulPhone>(await LoadUsefulPhones());
                PhoneCongregations = new List<PhoneCongregation>(await LoadPhoneCongregations());
                CollaboratorsList = ListCollaborator(string.Empty);
                RaisePropertyChanged(nameof(Collaborators));
                RaisePropertyChanged(nameof(UsefulPhones));
                RaisePropertyChanged(nameof(PhoneCongregations));
                UserDialogs.Instance.HideLoading();
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

            this.UsefulListTappedCommand = new Command(item =>
            {
                var usefulPhone = (UsefulPhone)item;
                Device.OpenUri(new Uri(string.Format("tel:{0}", usefulPhone.PhoneNumber)));
            });

            this.PhoneCongregationListTappedCommand = new Command(async item =>
            {
                var phoneCongregationItem = (PhoneCongregation)item;
                var navigationParams = new NavigationParameters
                {
                    { "PhoneCongregation", phoneCongregationItem }
                };

                CanNavigate = false;
                await _navigationService.NavigateAsync("CongregationPhonePage", navigationParams);
                CanNavigate = true;
            }, canExecute: (x) => CanNavigate);

            this.SearchCommand = new Command(item =>
            {   
                CollaboratorsList = ListCollaborator(SearchText);
            });
        }

        private void ExecuteRefreshCollaboratorCommand()
        {
            IsRefreshing = true;
            var collaboratorService = new CollaboratorService();
            Task.Run(async () =>
            {
                UserDialogs.Instance.ShowLoading(AppSettings.WaitingText, MaskType.Black);
                Collaborators = new List<Collaborator>(await collaboratorService.GetAllCollaboratorsFromApi());
                UsefulPhones = new List<UsefulPhone>(await LoadUsefulPhones());
                PhoneCongregations = new List<PhoneCongregation>(await LoadPhoneCongregations());
                CollaboratorsList = ListCollaborator(string.Empty);
                RaisePropertyChanged(nameof(Collaborators));
                RaisePropertyChanged(nameof(UsefulPhones));
                RaisePropertyChanged(nameof(PhoneCongregations));
                UserDialogs.Instance.HideLoading();
                DefaultToasts.Success("Dados atualizados com sucesso.");
                IsRefreshing = false;
            });
        }

        public IEnumerable<Group<string, Collaborator>> ListCollaborator(string filtro = "")
        {
            IEnumerable<Collaborator> filteredCollaborators = Collaborators;
            if (!string.IsNullOrEmpty(filtro))
                filteredCollaborators = this.Collaborators.Where(l => (l.Name.ToLower().Contains(filtro.ToLower())) || l.PositionMinistry.ToLower().Contains(filtro.ToLower()));

            return from collaborator in filteredCollaborators
                   orderby collaborator.PositionMinistry
                   group collaborator by collaborator.PositionMinistry into grupos
                   select new Group<string, Collaborator>(grupos.Key, grupos);
        }

        private async void ExecuteShowCategoriaCommand(Collaborator collaborator)
        {
            await _navigationService.NavigateAsync("Collaborator", new NavigationParameters(collaborator.Id.ToString()));
        }

        private async Task<IList<Collaborator>> LoadCollaborators()
        {
            var collaboratorService = new CollaboratorService();
            List<Collaborator> collaborators = new List<Collaborator>();

            collaborators = collaboratorService.GetAllCollaboratorsFromRealm().ToList();
            
            if(!collaborators.Any())
                collaborators = await collaboratorService.GetAllCollaboratorsFromApi();

            return collaborators;
        }

        private async Task<IList<UsefulPhone>> LoadUsefulPhones()
        {
            IList<UsefulPhone> usefulPhones = new List<UsefulPhone>();

            var api = new ApiUsefulPhoneService();

            usefulPhones = await api.GetAllUsefulPhones();

            return usefulPhones;
        }

        private async Task<IList<PhoneCongregation>> LoadPhoneCongregations()
        {
            IList<PhoneCongregation> phoneCongregations = new List<PhoneCongregation>();

            var api = new ApiPhoneCongregationService();

            phoneCongregations = await api.GetAllPhoneCongregations();

            return phoneCongregations;
        }
    }
}
