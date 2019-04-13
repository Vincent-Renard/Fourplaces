using System;
using System.Threading.Tasks;
using Model.Dtos;
using Storm.Mvvm;
using Xamarin.Forms;

namespace FourplacesApp.ViewModel
{
    public class PlaceViewViewModel : ViewModelBase
    {
        private PlaceItem _datPlace;
        private int _datID;
        public PlaceItem PlaceSelected
        {
            get => _datPlace;
            set => SetProperty(ref _datPlace, value);
        }

        public PlaceViewViewModel(int id_selected_place)
        {
            Console.WriteLine("PLACE VIEW VIEW MODEL");

            _datID = id_selected_place;
            Console.WriteLine("ID :"+_datID);

            base.OnResume();

        }
        public async override Task OnResume()
        {
            await base.OnResume();
            PlaceSelected = await App.API.GetPlace(_datID);
            Console.WriteLine("titre :" + PlaceSelected.Description); 
            Console.WriteLine("desc :" + PlaceSelected.Title);
        }
    }
}

