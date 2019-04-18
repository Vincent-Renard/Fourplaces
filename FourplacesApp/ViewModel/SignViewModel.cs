using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Model.Dtos;
using Storm.Mvvm;
using Xamarin.Forms;

namespace FourplacesApp.ViewModel
{
    public class SignViewModel : ViewModelBase
    {
        private INavigation Navigation;


        private bool _badLogin;

        private string _email;
        private string _password1;
        private string _password2;
        private string _firstName;
        private string _lastName;
        private string _badCredentials;


        public bool BadLogin
        {
            get => _badLogin;
            set => SetProperty(ref _badLogin, value);
        }
        public string EMail
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }


        public string Password1
        {
            get => _password1;
            set => SetProperty(ref _password1, value);
        }
        public string Password2
        {
            get => _password2;
            set => SetProperty(ref _password2, value);
        }
        public string BadCredentials
        {
            get => _badCredentials;
            set => SetProperty(ref _badCredentials, value);
        }
        public ICommand ToSign { get; set; }



        public SignViewModel(INavigation navigation)
        {
            Navigation = navigation;
            ToSign = new Command(async () =>
            {
                await RegisterAsync();
            });

        }

        async Task RegisterAsync()
        {
            Console.WriteLine("Sign test button");
            BadCredentials = "";
            BadLogin = false;
            if (EMail.Equals(""))
            {

                BadCredentials = "Mail vide" + Environment.NewLine;
                BadLogin = true;
                Console.WriteLine("Sign : mail pb");
            }
            if (FirstName.Equals(""))
            {
                BadCredentials = "Prénom vide" + Environment.NewLine;
                BadLogin = true;
                Console.WriteLine("Sign : fn pb");
            }
            if (LastName.Equals(""))
            {
                BadCredentials = "Nom vide" + Environment.NewLine;
                BadLogin = true;
                Console.WriteLine("Sign : ln pb");
            }
            if (!(Password1.Equals(Password2)))
            {
                BadCredentials = "Les mots de passe ne concordent pas" + Environment.NewLine;
                BadLogin = true;
                Console.WriteLine("Sign : pswd pb");
            }
            if (!BadLogin)
            {
                Console.WriteLine("Sign : no pb");
                RegisterRequest rr = new RegisterRequest
                {
                    Email = EMail,
                    FirstName = FirstName,
                    LastName = LastName,
                    Password = Password1
                };


                await App.API.Signin(rr);
                LoginRequest l = new LoginRequest
                {
                    Email = rr.Email,
                    Password = rr.Password
                };

                await App.API.Login(l);
                await Navigation.PushAsync(new Menu());

            }
            else
            {
                Console.WriteLine("Sign : pb maj ");
            }
        }





    }
}

