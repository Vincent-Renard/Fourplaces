using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FourplacesApp.ViewModel;
using Model;
using Model.Dtos;
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
        /*
        void OnSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {
            Console.WriteLine("ITEMTAPED");
            // Them cast the object SENDER to your Datasource Object, my case House House myHouse = sender as House; 
            var thePlace = sender as PlaceItemSummary;
           Console.WriteLine(thePlace.Title);
          Console.WriteLine(thePlace.Id);

        }*/


        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var plce = (PlaceItemSummary) e.SelectedItem;
            if (plce == null)
            {
              
                return;
            }
                
           

            await this.Navigation.PushAsync(new PlaceView(plce.Id));
        }

    }
}
