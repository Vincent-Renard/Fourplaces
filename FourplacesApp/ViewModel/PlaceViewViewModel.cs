﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.App;
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

        public bool Co
        {

            get => !(string.IsNullOrEmpty(App.API.LoginUser.Email));

        }
        public ICommand AddCommentary { get; set; }
        public List<CommentItem> ListeComms { get; set; }
        public Map Map { get; set; }
        public string CommentInput
        {
            get; set;
        }
        public string CommentaryOrPBHint
        {
            get; set;
        }

        public PlaceViewViewModel(int id_selected_place)
        {
            AddCommentary = new Command(async () => await AddComAsync());
            _datID = id_selected_place;
            Map = new Map
            {
                MapType = MapType.Street,
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

            if (!(string.IsNullOrWhiteSpace(CommentInput) || string.IsNullOrEmpty(CommentInput)))
            {
                CreateCommentRequest createComment = new CreateCommentRequest
                {
                    Text = CommentInput
                };
                await App.API.PostCommentAsync(PlaceSelected.Id, createComment);
                await base.OnResume();
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
            ListeComms = PlaceSelected.Comments;
            ListeComms.Sort((x, y) => y.Date.CompareTo(x.Date));
            Console.WriteLine("Liste comms Done [" + ListeComms.Count + "]");
            foreach (CommentItem c in ListeComms)
            {
                Console.WriteLine("[" + c.Author.FirstName + "] " + c.Text + " (" + c.Date + ")");
            }
        }
    }
}

