using Android.App;
using Android.Content.PM;
using Android.OS;
using Ninject;
using Prism.Ninject;
using Xfx;
using Acr.UserDialogs;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace AgendaCCB.App.Droid
{
    [Activity(Label = "Agenda CCB", Icon = "@drawable/icon", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);

            UserDialogs.Init(() => (Activity)Forms.Context);
            XfxControls.Init();

            DependencyService.Register<ImageButtonRenderer>();

            LoadApplication(new App(new AndroidInitializer()));
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IKernel kernel)
        {

        }
    }
}

