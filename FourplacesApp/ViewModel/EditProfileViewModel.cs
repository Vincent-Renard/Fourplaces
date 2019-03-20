using System;
using Storm.Mvvm;
using Xamarin.Forms;

namespace FourplacesApp.ViewModel
{
    public class EditProfileViewModel : ViewModelBase
    {
        private INavigation Navigation;
        public EditProfileViewModel(INavigation navigation)
        {

            Navigation = navigation;
          
        }

      
    }
}

