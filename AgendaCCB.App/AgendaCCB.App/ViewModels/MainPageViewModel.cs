﻿using Prism.Navigation;
using Xamarin.Forms;
using AgendaCCB.App.Controls;

namespace AgendaCCB.App.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _greetingTitle;
        public string GreetingTitle
        {
            get { return _greetingTitle; }
            set { SetProperty(ref _greetingTitle, value); }
        }

        private string _codeError;
        public string CodeError
        {
            get { return _codeError; }
            set { SetProperty(ref _codeError, value); }
        }

        private string _phoneNumberError;
        public string PhoneNumberError
        {
            get { return _phoneNumberError; }
            set { SetProperty(ref _phoneNumberError, value); }
        }

        public Command AcessarCommand { get; }

        public Command CadastroUsuarioCommand { get; }

        public string PhoneNumber { get; set; }
        public string Code { get; set; }

        public MainPageViewModel(INavigationService navigationService) :base(navigationService)
        {
            CadastroUsuarioCommand = new Command(ExecuteCadastroUsuarioCommand);
            AcessarCommand = new Command(ExecuteAcessarCommand);
            GreetingTitle = "A paz de Deus irmã(o)";
            Title = "Por favor confirme os dados para acesso";
        }

        private async void ExecuteAcessarCommand()
        {
            if (string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(Code))
            {
                if (string.IsNullOrEmpty(PhoneNumber))
                    PhoneNumberError = "Digite um número";
                else
                    PhoneNumberError = string.Empty;
                if (string.IsNullOrEmpty(Code))
                    CodeError = "Digite o código";
                else
                    CodeError = string.Empty;
            }
            else
                await _navigationService.NavigateAsync("MainPrincipal");
        }

        private async void ExecuteCadastroUsuarioCommand()
        {
            await _navigationService.NavigateAsync("CadastroUsuario");
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("title"))
                Title = (string)parameters["title"] + " and Prism";
        }

        public void AcessarButtonPage()
        {

        }
    }
}
