using System.Threading.Tasks;
using System.Windows.Input;
using Model.Dtos;
using Xamarin.Forms;
using Storm.Mvvm;
using System;

namespace FourplacesApp.ViewModel
{
    internal class ConnectionViewModel: ViewModelBase
    {
        private INavigation Navigation;
        private string _email;
        private string _password;

        private bool _badLogin;
        private string _badCredentials;

        public string BadCredentials
        {
            get => _badCredentials;
            set => SetProperty(ref _badCredentials, value);
        }
        public bool BadLogin
        {
            get => _badLogin;
            set => SetProperty(ref _badLogin, value);
        }
        public string Mail
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }



        public ICommand Connexion { get; set; }
        public ICommand GoToRegister { get; set; }


        public ConnectionViewModel(INavigation navigation)
        {

            this.Navigation = navigation;
            Connexion = new Command(async () => await TestCoAsync());
            GoToRegister = new Command(async () => await GoRegisterAsync());
        }
        async Task GoRegisterAsync()
        {//TODO
            await Navigation.PushAsync(new Sign());
        }

        async Task TestCoAsync()
        {

            LoginRequest l = new LoginRequest
            {
                Email = _email,
                Password = _password
            };
            if (!(await App.rs.Login(l)))
            {
                BadCredentials = "Mauvais mail/password";
                BadLogin = true;
            }
            else
            {
                BadLogin = false;
                //await Navigation.PushAsync(new HomePage());
            }
        }
      


    }
}