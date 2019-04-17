using System;
using System.Collections.Generic;
using FourplacesApp.ViewModel;
using Plugin.Media.Abstractions;
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
