using Prism.Ninject;
using AgendaCCB.App.Views;
using Xamarin.Forms;

namespace AgendaCCB.App
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<CadastroUsuario>();
            Container.RegisterTypeForNavigation<MainPrincipal>();
        }
    }
}
