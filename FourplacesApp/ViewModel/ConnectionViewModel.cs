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
        private INavigation navigation;
        private string _username;
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
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }



        public ICommand connexion { get; set; }
        public ICommand goToRegister { get; set; }


        public ConnectionViewModel(INavigation navigation)
        {

            this.navigation = navigation;
            connexion = new Command(async () => await testCoAsync());
            goToRegister = new Command(async () => await GoRegisterAsync());
        }

        private Task GoRegisterAsync()
        {//TODO
            throw new NotImplementedException();
        }

        async Task testCoAsync()
        {
            LoginRequest l = new LoginRequest
            {
                Email = _username,
                Password = _password
            };
            if (!(await App.rs.Login(l)))
            {
                BadCredentials = "Mauvais mail/password";
                BadLogin = true;
            }
            else
            {
                BadLogin = true;
            }
        }
      


    }
}