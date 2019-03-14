using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Storm.Mvvm;
using Xamarin.Forms;

namespace FourplacesApp.ViewModel
{
    public class SignViewModel : ViewModelBase
    {
        private INavigation Navigation;

  
        private bool _badLogin=false;

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
            this.Navigation = navigation;
            this.ToSign = new Command(async () => await RegisterAsync());

        }

        async Task RegisterAsync()
        {
            BadCredentials = "";
           
            if (EMail.Length == 0)
            {
                BadCredentials = "Mail vide" + Environment.NewLine;
                BadLogin = true;
            }
            if (FirstName.Length == 0)
            {
                BadCredentials = "Prénom vide" + Environment.NewLine;
                BadLogin = true;
            }
            if (LastName.Length == 0)
            {
                BadCredentials = "Nom vide" + Environment.NewLine;
                BadLogin = true;
            }
            if (!(Password1.Equals(Password2)))
            {
                BadCredentials = "Les mots de passe ne concordent pas" + Environment.NewLine;
                BadLogin = true;
            }
            if(!BadLogin)
            {
                  
                await Navigation.PushAsync(new Home());
            }
        }
            /*

    {
    "email": "string",
    "first_name": "string",
    "last_name": "string",
    "password": "string"
    }

    */




        }
}

