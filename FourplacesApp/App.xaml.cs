using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FourplacesApp
{
    public partial class App : Application
    {

        public static RestService API;
        public App()
        {
            API = new RestService();
            InitializeComponent();
            MainPage = new NavigationPage(new Connection());
          // MainPage = new NavigationPage(new Sign());

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
