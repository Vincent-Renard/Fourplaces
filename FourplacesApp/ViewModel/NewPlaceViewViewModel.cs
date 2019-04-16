using System;
using Storm.Mvvm;
using Xamarin.Forms;

namespace FourplacesApp.ViewModel
{
    public class NewPlaceViewViewModel : ViewModelBase
    {
        private INavigation Navigation;
        private string _descPlace;
        public string DescriptionPlace
        {
            get => _descPlace;
            set => SetProperty(ref _descPlace, value);
        }
        private string _titrePlace;
        public string TitrePlace
        {
            get => _titrePlace;
            set => SetProperty(ref _titrePlace, value);
        }
        public NewPlaceViewViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }
    }
}

