using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Model.Dtos;
using Storm.Mvvm;

using Xamarin.Forms;

namespace FourplacesApp.ViewModel
{
    public class UpdatePasswordViewModel : ViewModelBase
    {
        private INavigation Navigation;


        private bool _badLogin = false;
        private string _password1;
        private string _password2;
        private string _badCredentials;


        public bool BadMdp
        {
            get => _badLogin;
            set => SetProperty(ref _badLogin, value);
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
        public ICommand Vald { get; set; }




        public UpdatePasswordViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Vald = new Command(async () => await UpdatePassw());

        }


        async Task UpdatePassw()
        {

            BadCredentials = "";
            BadMdp = false;



            if (!(Password1.Equals(Password2)))
            {
                BadCredentials = "Les mots de passe ne concordent pas" + Environment.NewLine;
                BadMdp = true;
                Console.WriteLine(" pswd pb");
            }
            if (!BadMdp)
            {



                await App.API.PatchPassword(Password1);

                await Navigation.PopAsync();

            }
            else
            {
                Console.WriteLine("Sign : pb maj ");
            }
        }





    }

}

