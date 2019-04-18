using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Model.Dtos;
using Storm.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FourplacesApp.ViewModel
{
    public class PlaceViewViewModel : ViewModelBase
    {
        private INavigation Navigation;
        private PlaceItem _datPlace;
        private readonly int _datID;
        public PlaceItem PlaceSelected
        {
            get => _datPlace;
            set => SetProperty(ref _datPlace, value);
        }

        public bool Co => !(string.IsNullOrEmpty(App.API.LoginUser.Email));

        public ICommand AddCommentary { get; set; }
       

        private List<CommentItem> _listeComms;

        public List<CommentItem> ListeComms
        {
            get => _listeComms;
            set => SetProperty(ref _listeComms, value);
        }

        public Map Map { get; set; }
        public string CommentInput { get; set; }
        public string CommentaryOrPBHint { get; set; }

        public PlaceViewViewModel(int id_selected_place,INavigation nav)
        {
            Navigation = nav;
            AddCommentary = new Command(async () => await AddComAsync());
            _datID = id_selected_place;
            Map = new Map
            {
                MapType = MapType.Street
            };
            if (Co)
            {
                CommentaryOrPBHint = "Ajouter un commentaire";
            }
            else
            {
                CommentaryOrPBHint = "Connexion requise";
            }
           base.OnResume();

        }
        async Task AddComAsync()
        {
            if (Co)
            {

                if (!(string.IsNullOrWhiteSpace(CommentInput) || string.IsNullOrEmpty(CommentInput)))
                {
                    CreateCommentRequest createComment = new CreateCommentRequest
                    {
                        Text = CommentInput
                    };
                    await App.API.PostCommentAsync(PlaceSelected.Id, createComment);
                    CommentInput = "";
                    await base.OnResume();
                }
            }
            else
            {
                await Navigation.PushAsync(new Connection());
            }
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
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(App.RadiusMap)));

            Console.WriteLine("Liste comms");
            List<CommentItem> temp = PlaceSelected.Comments;


            temp.Sort((x, y) => y.Date.CompareTo(x.Date));

            foreach (CommentItem c in temp)
            {
                Console.WriteLine("[" + c.Author.FirstName + "] " + c.Text + " (" + c.Date + ")");
            }

            ListeComms = temp;
            Console.WriteLine("Affichage");

        }
    }
}

