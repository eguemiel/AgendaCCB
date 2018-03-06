using AgendaCCB.App.Services;
using AgendaCCB.App.Services.Commom;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace AgendaCCB.App.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware
    {
        protected INavigationService _navigationService { get; }

        protected readonly IAgendaCCBApiService _agendaCCBApiService;

        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _agendaCCBApiService = DependencyService.Get<IAgendaCCBApiService>();
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _hasConnection;
        public bool HasConnection
        {
            get { return _hasConnection; }
            set { SetProperty(ref _hasConnection, value); }
        }

        private bool _canNavigate = true;
        public bool CanNavigate
        {
            get { return _canNavigate; }
            set { SetProperty(ref _canNavigate, value); }
        }

        private bool _canExecute = true;
        public bool CanExecute
        {
            get { return _canExecute; }
            set { SetProperty(ref _canExecute, value); }
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}
