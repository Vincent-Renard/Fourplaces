using System;
using System.Windows.Input;
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
            this.Navigation = navigation;

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

