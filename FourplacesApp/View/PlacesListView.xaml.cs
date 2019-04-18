using Model;
using Storm.Mvvm.Forms;
using Xamarin.Forms;

namespace FourplacesApp
{
    public partial class PlacesListView : BaseContentPage
    {
        public PlacesListView()
        {
            InitializeComponent();
            BindingContext = new PlacesListViewModel(Navigation);
        }


        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var plce = (PlaceItemSummary)e.SelectedItem;
            if (plce == null)
            {
                return;
            }
            await this.Navigation.PushAsync(new PlaceView(plce.Id));
        }

    }
}
