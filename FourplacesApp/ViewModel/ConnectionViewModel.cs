using System.Threading.Tasks;
using System.Windows.Input;
using Model.Dtos;
using Xamarin.Forms;
using Storm.Mvvm;
using System;
using System.Collections.Generic;
using Model;

namespace FourplacesApp.ViewModel
{
    public partial class ConnectionViewModel : ViewModelBase
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

            Navigation = navigation;
            Connexion = new Command(async () => await ConnexionAsync());
            GoToRegister = new Command(async () => await GoRegisterAsync());
        }


        async Task GoRegisterAsync()
        {
            Console.WriteLine("Go register");
            await Navigation.PushAsync(new Sign());
        }

        async Task ConnexionAsync()
        {
            Console.WriteLine("Connexion...");
            if (string.IsNullOrEmpty(_email) && string.IsNullOrEmpty(_password))
            {
                _email = "mail@mail.com";
                _password = "mdp";
            }



            LoginRequest l = new LoginRequest
            {
                Email = _email,
                Password = _password
            };
            if (!(await App.API.Login(l)))
            {
                BadCredentials = "Mauvais mail/password";
                BadLogin = true;
            }
            else
            {
                BadLogin = false;
                await Navigation.PushAsync(new Menu());
            }
        }



    }
}