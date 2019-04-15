using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Dtos;
using Storm.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

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
        private List<CommentItem> _listeComms;

        public List<CommentItem> ListeComms
        {
            get => _listeComms;
            set => SetProperty(ref _listeComms, value);
        }
        public Map Map { get; set; }

        public PlaceViewViewModel(int id_selected_place)
        {
         
            _datID = id_selected_place;
            Map = new Map
            {
                MapType = MapType.Street,
                //IsShowingUser = true //pour centrer la map sur la position de l'utilisateur 

        };

            //TEST MAP
          

            base.OnResume();

        }
        public async override Task OnResume()
        {
            await base.OnResume();
            PlaceSelected = await App.API.GetPlace(_datID);

            var position = new Position(PlaceSelected.Latitude, PlaceSelected.Longitude); // Latitude, Longitude
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = PlaceSelected.Title

            };
            Map.Pins.Add(pin);
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(8)));


            ListeComms = PlaceSelected.Comments;
            ListeComms.Sort((x, y) => y.Date.CompareTo(x.Date));
          





        }
    }
}

