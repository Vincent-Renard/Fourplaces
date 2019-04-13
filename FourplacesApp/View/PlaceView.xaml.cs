using System;
using System.Collections.Generic;
using FourplacesApp.ViewModel;
using Model.Dtos;
using Storm.Mvvm.Forms;
using Xamarin.Forms;

namespace FourplacesApp
{
    public partial class PlaceView : BaseContentPage
    {
        public int Id_sel;
        public PlaceView(int id_selected_place)
        {

            InitializeComponent();
            Id_sel = id_selected_place;
            BindingContext = new PlaceViewViewModel(Id_sel);
        }
    }

}
