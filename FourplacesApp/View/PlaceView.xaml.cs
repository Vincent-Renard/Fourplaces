using System;
using System.Collections.Generic;
using FourplacesApp.ViewModel;
using Storm.Mvvm.Forms;
using Xamarin.Forms;

namespace FourplacesApp
{
    public partial class PlaceView : BaseContentPage
    {
        public PlaceView()
        {
            InitializeComponent();
            BindingContext = new PlaceViewViewModel();
        }
    }

}
