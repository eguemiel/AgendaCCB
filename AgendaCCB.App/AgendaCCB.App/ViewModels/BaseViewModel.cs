using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaCCB.App.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware
    {
        protected INavigationService _navigationService { get; }

        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _canNavigate = true;
        public bool CanNavigate
        {
            get { return _canNavigate; }
            set { SetProperty(ref _canNavigate, value); }
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
