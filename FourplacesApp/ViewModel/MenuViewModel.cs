using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Storm.Mvvm;
using Xamarin.Forms;

namespace FourplacesApp.ViewModel
{
    public class MenuViewModel : ViewModelBase
    {


        private INavigation Navigation;
        public ICommand Connexion { get; set; }
        public ICommand PlacesList { get; set; }
        public ICommand MyProfile { get; set; }
        public ICommand AddPlace { get; set; }
        public ICommand Deconnexion { get; set; }
        private string needLogin;
        private bool noCo;

        public bool NoCo
        {

            get => string.IsNullOrEmpty(App.API.LoginUser.Email);
            set => SetProperty(ref noCo, value);
        }
        public bool Co
        {

            get => !NoCo;
        }

        public string NeedLogin
        {
            get => needLogin;
            set => SetProperty(ref needLogin, value);
        }


        public MenuViewModel(INavigation navigation)
        {

            Navigation = navigation;
            Connexion = new Command(async () => await ConnexionAsync());
            Deconnexion = new Command(() => GoDeconnexion());
            PlacesList = new Command(async () => await GoShowPlacesListAsync());
            AddPlace = new Command(async () => await AddPlaceAsync());
            MyProfile = new Command(async () => await GoMyProfileAsync());

        }
        public override async Task OnResume()
        {
            Console.WriteLine("OnResume");
            if (Co)
                needLogin = "";
            else
                needLogin = "Connection requise";

            await base.OnResume();

        }
        async Task AddPlaceAsync()
        {
            await Navigation.PushAsync(new NewPlaceView());
        }
        async Task ConnexionAsync()
        {
            await Navigation.PushAsync(new Connection());
        }

        async Task GoShowPlacesListAsync()
        {

            await Navigation.PushAsync(new PlacesListView());



        }

        async Task GoMyProfileAsync()
        {
            if (Co)
            {
                //await Navigation.PushAsync(new PlaceView(56));
                await Navigation.PushAsync(new EditProfile());
            }
        }

        void GoDeconnexion()
        {
            App.API.LoginUser.Email = null;
            App.API.LoginUser.Password = null;

        }


    }
}

