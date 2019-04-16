using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Model;
using Model.Dtos;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FourplacesApp
{
    public partial class App : Application
    {
        public static RestService API;
        public static readonly double RadiusMap = 2.5;
        private static Position defaultPosition;

        public App()
        {

            API = new RestService();
            InitializeComponent();
            MainPage = new NavigationPage(new Menu());
            defaultPosition = new Position(47.845647, 1.939958);

        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static async Task<Position> LocalisationAsync()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                if (CrossGeolocator.IsSupported && CrossGeolocator.Current.IsGeolocationEnabled && CrossGeolocator.Current.IsGeolocationAvailable)
                {

                    var ll = await locator.GetPositionAsync();
                    Position p = new Position(ll.Latitude, ll.Longitude);

                    Console.WriteLine("vraie pos ");


                    return p;
                }
                var ll2 = await locator.GetLastKnownLocationAsync();
                Console.WriteLine("last pos ");
                return new Position(ll2.Latitude, ll2.Longitude);

            }
            catch (Exception)
            {

                Console.WriteLine("Error localistaion position par default donc ");
                return defaultPosition;
            }
        }

    }
}
