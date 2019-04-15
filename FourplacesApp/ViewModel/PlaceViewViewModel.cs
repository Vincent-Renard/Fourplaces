using System;
using System.Collections.Generic;
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
        private List<CommentItem> _listeComms;

        public List<CommentItem> ListeComms
        {
            get => _listeComms;
            set => SetProperty(ref _listeComms, value);
        }
        public PlaceViewViewModel(int id_selected_place)
        {
         
            _datID = id_selected_place;
           
            base.OnResume();

        }
        public async override Task OnResume()
        {
            await base.OnResume();
            PlaceSelected = await App.API.GetPlace(_datID);
            /*
            foreach(CommentItem c in PlaceSelected.Comments)
            {
                Console.WriteLine("A: "+c.Author.FirstName+" c:"+c.Text);
            }
            */

            ListeComms = PlaceSelected.Comments;
            ListeComms.Sort((x, y) => y.Date.CompareTo(x.Date));
        }
    }
}

