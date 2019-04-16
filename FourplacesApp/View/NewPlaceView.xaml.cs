using System;
using System.Collections.Generic;
using FourplacesApp.ViewModel;
using Storm.Mvvm.Forms;
using Xamarin.Forms;

namespace FourplacesApp
{
    public partial class NewPlaceView : BaseContentPage
    {
        public NewPlaceView()
        {
            InitializeComponent();
            BindingContext = new NewPlaceViewViewModel(Navigation);
        }
    }
}
