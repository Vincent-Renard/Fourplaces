using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Model;
using Model.Dtos;
using Plugin.Geolocator;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
                locator.DesiredAccuracy = 100;
                if (CrossGeolocator.IsSupported && CrossGeolocator.Current.IsGeolocationEnabled && CrossGeolocator.Current.IsGeolocationAvailable)
                {

                    var ll = await locator.GetPositionAsync();
                    Position p = new Position(ll.Latitude, ll.Longitude);

                    Console.WriteLine("LOCALISATION vraie pos ");


                    return p;
                }
                var ll2 = await locator.GetLastKnownLocationAsync();
                Console.WriteLine("LOCALISATION last pos ");
                return new Position(ll2.Latitude, ll2.Longitude);

            }
            catch (Exception)
            {

                Console.WriteLine("LOCALISATION Error localistaion position par defaut donc ");
                return defaultPosition;
            }
        }
        public static async Task<MediaFile> PickAPic()
        {
            await CrossMedia.Current.Initialize();
            if (CrossMedia.Current.IsPickPhotoSupported)
            { 

                return await CrossMedia.Current.PickPhotoAsync();
            }
            Console.WriteLine("On a pas pic the pic ");
            return null;
        }
    }
}
