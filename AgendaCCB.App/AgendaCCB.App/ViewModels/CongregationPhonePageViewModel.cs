using AgendaCCB.App.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace AgendaCCB.App.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class CongregationPhonePageViewModel : BaseViewModel
    {
        public PhoneCongregation PhoneCongregation { get; set; }

        public Command ItemClickedCommand { get; set; }

        public CongregationPhonePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Detalhes Congregação";          
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            PhoneCongregation = (PhoneCongregation)parameters["PhoneCongregation"] != null ? (PhoneCongregation)parameters["PhoneCongregation"] : PhoneCongregation;
        }
    }
}
