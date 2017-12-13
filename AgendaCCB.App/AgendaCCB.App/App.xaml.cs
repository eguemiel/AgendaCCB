using Prism.Ninject;
using AgendaCCB.App.Views;
using Xamarin.Forms;
using AgendaCCB.App.Services.AppServices;
using AgendaCCB.App.Models;
using AgendaCCB.App.Services;
using AgendaCCB.App.Services.Commom;
using AgendaCCB.App.Helpers;

namespace AgendaCCB.App
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            var userService = new UserService();
            UserAppSession userAppSession = userService.GetUsuario(true);
            AgendaCCBApiService.BehaviorIfNotLogged = async () =>
            {
                await NavigationService.NavigateAsync("NavigationPage/MainPage");
            };

            if (userAppSession.Logged)
            {
                NavigationService.NavigateAsync($"NavigationPage/{AppSettings.HomeApplication}");
            }
            else
            {
                NavigationService.NavigateAsync("NavigationPage/MainPage");
            }
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<MainPrincipal>();
            Container.RegisterTypeForNavigation<CollaboratorPage>();
        }
    }
}
