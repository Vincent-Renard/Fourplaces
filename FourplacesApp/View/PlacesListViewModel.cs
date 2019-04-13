using Xamarin.Forms;

namespace FourplacesApp
{
    internal class PlacesListViewModel
    {
        private INavigation navigation;

        public PlacesListViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }
    }
}