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
            Valider = new Command(async () => await UpdateUser());
            UpdatePassword = new Command(async () => await GoUpdatePassord());
            RemplirLasts();

        }
        public async override Task OnResume()
        {
            RemplirLasts();
            await base.OnResume();
        }
        async Task GoUpdatePassord()
        {
            await Navigation.PushAsync(new UpdatePassword());
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

        private void RemplirLasts()
        {

            UserItem lastMe = App.API.UserItem;
            LastLastName = lastMe.LastName;
            LastFirstName = lastMe.FirstName;
            Console.WriteLine(LastLastName);
            Console.WriteLine(LastFirstName);
        }

    }
}

