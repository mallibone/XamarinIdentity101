using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OidcSample
{
    public partial class App : Application
    {
        oidcxamarin101:/authenticated
        public const string CallbackUri = "oidcxamarin101";
        public static readonly string CallbackScheme = $"{CallbackUri}:/authenticated";
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
