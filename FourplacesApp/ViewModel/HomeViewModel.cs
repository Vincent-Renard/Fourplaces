using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Model.Dtos;
using Storm.Mvvm;
using Xamarin.Forms;
using Model;

namespace FourplacesApp.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private INavigation Navigation;

        private List<PlaceItemSummary> _liste;

        public List<PlaceItemSummary> Liste
        {
            get => _liste;
            set => SetProperty(ref _liste, value);
        }

        public HomeViewModel(INavigation navigation)
        {
            Navigation = navigation;
       


      
        

        }
        public async override Task OnResume()
        {
            await base.OnResume();
      
            Liste = await App.API.GetListPlacesAsync();
        }
    }
}

