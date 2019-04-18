using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Model.Dtos;
using Plugin.Media.Abstractions;
using Storm.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FourplacesApp.ViewModel
{
    public class NewPlaceViewViewModel : ViewModelBase
    {
        private INavigation Navigation;
        private string _descPlace;
        private string _imageSrc;
        private MediaFile picture;
        public string ImageSrc
        {
            get => _imageSrc;
            set => SetProperty(ref _imageSrc, value);
        }

        public string DescriptionPlace
        {
            get => _descPlace;
            set => SetProperty(ref _descPlace, value);
        }
        private string _titrePlace;
        private Position posOfTheplace;
        public string TitrePlace
        {
            get => _titrePlace;
            set => SetProperty(ref _titrePlace, value);
        }
        public Map Map { get; set; }
        public ICommand Send { get; set; }
        public ICommand SendImg { get; set; }
        public NewPlaceViewViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Map = new Map
            {
                MapType = MapType.Street
            };
            Map.Pins.Clear();
            Send = new Command(async () =>
            {
                await AddAPlaceAsync();
            });
            SendImg = new Command(async () =>
            {
                await InsertImageAsync();
            });
             
        }

        private async Task AddAPlaceAsync()
        {
            if (!string.IsNullOrWhiteSpace(TitrePlace))
            {
                CreatePlaceRequest placeRequest = new CreatePlaceRequest
                {
                    Title = TitrePlace,
                    Description = DescriptionPlace
                    , Latitude = posOfTheplace.Latitude,
                    Longitude = posOfTheplace.Longitude
                };
                if (!string.IsNullOrEmpty(ImageSrc))
                {
                    placeRequest.ImageId = await App.API.PostImgAsync(picture);
                }
                await App.API.PostPlaceAsync(placeRequest);
            }
            await base.OnResume();
            await Navigation.PopAsync();
        }

        public async override Task OnResume()
        {
            await base.OnResume();
            posOfTheplace = await App.LocalisationAsync();

            Pin p = new Pin { Position = posOfTheplace, Type = PinType.Place };
            Map.Pins.Add(p);
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(posOfTheplace, Distance.FromKilometers(App.RadiusMap)));
        
        }
        async Task InsertImageAsync()
        {
            MediaFile pic = await App.PickAPic();
            if (pic == null)

                ImageSrc = null;
            else
            {
                picture = pic;
                ImageSrc = pic.Path;
            }




        }
    }
}

