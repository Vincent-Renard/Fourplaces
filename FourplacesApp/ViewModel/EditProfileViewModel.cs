using System;
using System.Threading.Tasks;
using System.Windows.Input;
using FourplacesApp;
using Model.Dtos;
using Plugin.Media.Abstractions;
using Storm.Mvvm;
using Xamarin.Forms;

namespace FourplacesApp.ViewModel
{
    public class EditProfileViewModel : ViewModelBase
    {
        private INavigation Navigation;
        private MediaFile profilePicture;
        private string _firstName;
        private string _lastName;
        private string _LastFirstName;
        private string _LastLastName;
        private string _imgProfil;

        public string ImageSrc
        {
            get => _imgProfil;
            set => SetProperty(ref _imgProfil, value);
        }

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

     
       
        public ICommand SendProfilePic { get; set; }
        public EditProfileViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Valider = new Command(async () => await UpdateUser());
            UpdatePassword = new Command(async () => await GoUpdatePassord());
            SendProfilePic = new Command(async () => await InsertImageAsync());

            RemplirLasts();

        }

     
        public async override Task OnResume()
        {
            await base.OnResume();
            RemplirLasts();
           
        }
        async Task GoUpdatePassord()
        {
            await Navigation.PushAsync(new UpdatePassword());
        }

        async Task UpdateUser()
        {
            UpdateProfileRequest nouveau = new UpdateProfileRequest();

            if (!(string.IsNullOrWhiteSpace(InputFirstName)||string.IsNullOrEmpty(InputFirstName)))
            {
                nouveau.FirstName = InputFirstName;
            }

            if (!(string.IsNullOrWhiteSpace(InputLastName) || string.IsNullOrEmpty(InputLastName)))
            {
                nouveau.LastName = InputLastName;
            }

           
            if (!string.IsNullOrEmpty(ImageSrc))
            {
                nouveau.ImageId = await App.API.PostImgAsync(profilePicture);
            }
            Console.WriteLine("up me ");
            await App.API.PatchMe(nouveau);
            Console.WriteLine("Retour ");
            RemplirLasts();
            await OnResume();

        }
        async Task InsertImageAsync()
        {
            MediaFile pic = await App.PickAPic();
            if (pic == null)

                ImageSrc = null;
            else
            {
                profilePicture = pic;
                ImageSrc = pic.Path;
            }

        }
        private void RemplirLasts()
        {

            UserItem lastMe = App.API.UserItem;
            LastLastName = lastMe.LastName;
            LastFirstName = lastMe.FirstName;
            ImageSrc = App.API.GetImage(lastMe.ImageId);

            Console.WriteLine(lastMe.FirstName);
            Console.WriteLine(lastMe.LastName);
            Console.WriteLine(lastMe.ImageId);
            Console.WriteLine(ImageSrc);
            Console.WriteLine(App.API.GetImage(lastMe.ImageId));
        }

    }
}

