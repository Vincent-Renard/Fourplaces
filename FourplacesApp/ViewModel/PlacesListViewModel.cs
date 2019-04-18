using System.Collections.Generic;
using System.Threading.Tasks;
using Storm.Mvvm;
using Xamarin.Forms;
using Model;
using System;
using Xamarin.Forms.Maps;

namespace FourplacesApp
{
    public class PlacesListViewModel : ViewModelBase
    {
        private INavigation Navigation;
        public Map MapAll { get; set; }
        private List<PlaceItemSummary> _liste;

        public List<PlaceItemSummary> Liste
        {
            get => _liste;
            set => SetProperty(ref _liste, value);
        }

        public PlacesListViewModel(INavigation navigation)
        {
            Navigation = navigation;
            MapAll = new Map
            {
                MapType = MapType.Street
            };

            base.OnResume();

        }

        public async override Task OnResume()
        {
            await base.OnResume();
            Liste = await App.API.GetListPlacesAsync();
            Position user_position = await App.LocalisationAsync();
            foreach (PlaceItemSummary p in Liste)
            {
                Console.WriteLine(p.Title);
                var position = new Position(p.Latitude, p.Longitude); // Latitude, Longitude
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = position,
                    Label = p.Title
                };
                MapAll.Pins.Add(pin);
                MapAll.MoveToRegion(MapSpan.FromCenterAndRadius(user_position, Distance.FromKilometers(App.RadiusMap)));
            }
        }
    }
}

