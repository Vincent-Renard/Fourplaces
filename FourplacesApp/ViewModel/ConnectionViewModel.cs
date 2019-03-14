using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Model.Dtos;
using Xamarin.Forms;

namespace FourplacesApp
{
    internal class ConnectionViewModel
    {
        private INavigation navigation;
        private string _username;
        private string _password;



        public ICommand connexion { get; set; }
        public ICommand register { get; set; }


        public ConnectionViewModel(INavigation navigation)
        {

            this.navigation = navigation;
            connexion = new Command(async () => await testCoAsync());
            async Task testCoAsync()
            {
                LoginRequest l = new LoginRequest();
                l.Email = _username;
                l.Password = _password;
                LoginResult reponse = await App.rs.Login(l);

            }
        }


    }
}