using AgendaCCB.App.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgendaCCB.App.ViewModels
{
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
