using System;
using System.Threading.Tasks;
using System.Windows.Input;
using FourplacesApp;
using Model.Dtos;
using Storm.Mvvm;
using Xamarin.Forms;

namespace FourplacesApp.ViewModel
{
    public class EditProfileViewModel : ViewModelBase
    {
        private INavigation Navigation;
        /*
        private string _password1;
        private string _password2;

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
       */

        private string _firstName;
        private string _lastName;
        private string _LastFirstName;
        private string _LastLastName;

        public string InputFirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string InputLastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }
       


        public string LastFirstName
        {
            get => _LastFirstName;
            set => SetProperty(ref _LastFirstName, value);
        }

        public string LastLastName
        {
            get => _LastLastName;
            set => SetProperty(ref _LastLastName, value);
        }
        public ICommand Valider { get; set; }
        public ICommand UpdatePassword { get; set; }

        public EditProfileViewModel(INavigation navigation)
        {

            Navigation = navigation;
            Valider =new Command(async () => await UpdateUser());
            Valider = new Command(async () => await GoUpdatePassord());
            RemplirLasts();
          
        }

        async Task GoUpdatePassord()
        {
           await  Navigation.PushAsync(new UpdatePassword());
        }

        async Task UpdateUser()
        {
            UpdateProfileRequest nouveau = new UpdateProfileRequest
            {
                FirstName = InputFirstName,
                LastName = InputLastName
            };
            await App.API.PatchMe(nouveau);
            RemplirLasts();
            
        }

        private async void RemplirLasts()
        {
            UserItem lastMe = await App.API.GetMe();
            LastLastName = lastMe.LastName;
            LastFirstName = lastMe.FirstName;
        }

    }
}

