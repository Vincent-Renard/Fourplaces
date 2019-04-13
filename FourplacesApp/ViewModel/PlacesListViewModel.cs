using System.Collections.Generic;
using System.Threading.Tasks;
using Storm.Mvvm;
using Xamarin.Forms;
using Model;

namespace FourplacesApp
{
    public class PlacesListViewModel : ViewModelBase
    {
        private INavigation Navigation;

        private List<PlaceItemSummary> _liste;

        public List<PlaceItemSummary> Liste
        {
            get => _liste;
            set => SetProperty(ref _liste, value);
        }

        public PlacesListViewModel(INavigation navigation)
        {
            Navigation = navigation;
            base.OnResume();

        }
        public async override Task OnResume()
        {
            await base.OnResume();
            Liste = await App.API.GetListPlacesAsync();



        }
    }
}

