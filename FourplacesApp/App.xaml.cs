using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FourplacesApp
{
    public partial class App : Application
    {

        public static RestService rs;
        public App()
        {
            rs = new RestService();
            InitializeComponent();
            MainPage = new NavigationPage(new Connection());
            rs.GetRoot();
            
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
