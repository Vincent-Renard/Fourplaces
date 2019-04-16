using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Storm.Mvvm;
using Xamarin.Forms;

namespace FourplacesApp.ViewModel
{
    public class MenuViewModel : ViewModelBase
    {

        public string MailUser
        {
            get => App.API.LoginUser.Email;

        }
        private INavigation Navigation;
        public ICommand Connexion { get; set; }
        public ICommand Deconnexion { get; set; }
        public ICommand GoHome { get; set; }
        public ICommand MyProfile { get; set; }

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
            needLogin = "Connection requise";
            Navigation = navigation;
            Connexion = new Command(async () => await ConnexionAsync());
            Deconnexion = new Command(() => GoDeconnexion());
            GoHome = new Command(async () => await GoHomeAsync());
            MyProfile = new Command(async () => await GoMyProfileAsync());
        }
        public override Task OnResume()
        {
            if (!NoCo)
            {
                needLogin = "";
            }
            return base.OnResume();
        }

        async Task ConnexionAsync()
        {
            await Navigation.PushAsync(new Connection());
        }

        async Task GoHomeAsync()
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

