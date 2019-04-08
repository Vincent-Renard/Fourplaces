using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.App;
using Android.Widget;
using Storm.Mvvm;
using Xamarin.Forms;

namespace FourplacesApp.ViewModel
{
    public class MenuViewModel : ViewModelBase
    {
        public static string MailUser { get; set; }
        private INavigation Navigation;
        public ICommand Connexion { get; set; }
        public ICommand Deconnexion { get; set; }
        public ICommand GoHome { get; set; }
        public ICommand MyProfile { get; set; }
        public MenuViewModel(INavigation navigation)
        {
            MailUser = "";
            Navigation = navigation;
            Connexion = new Command(async () => await ConnexionAsync());
            Deconnexion = new Command( () =>  GoDeconnexion());
            GoHome = new Command(async () => await GoHomeAsync());
            MyProfile = new Command(async () => await GoMyProfileAsync());
        }

        async Task ConfirmationConnection()
        {
            //TODO faire un toast pour exiger connection
        }

        async Task ConnexionAsync()
        {
            await Navigation.PushAsync(new Connection());
        }

        async Task GoHomeAsync()
        {
            if (MailUser.Length == 0)
            {
                //TODO mettre un toast pour exiger connection
            }else
            
                await Navigation.PushAsync(new Home());
            
           

        }

        async Task GoMyProfileAsync()
        {
            if (MailUser.Length == 0)
            {
                //TODO mettre un toast pour exiger connection
            }
            else
            
                await Navigation.PushAsync(new EditProfile());
        }

         void GoDeconnexion()
        {
            MailUser = "";
        }


    }
}

