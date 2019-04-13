using System;
using System.Collections.Generic;
using FourplacesApp.ViewModel;
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
    }
}
