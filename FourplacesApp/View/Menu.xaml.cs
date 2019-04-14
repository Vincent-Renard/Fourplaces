﻿using System;
using System.Collections.Generic;
using FourplacesApp.ViewModel;
using Storm.Mvvm.Forms;
using Xamarin.Forms;

namespace FourplacesApp
{
    public partial class Menu : BaseContentPage
    {
        public Menu()
        {
            InitializeComponent();
            BindingContext = new MenuViewModel(Navigation);
        }
        public async void OnNoCo()
        {
            await DisplayAlert("Connexion requise", "Veuillez vous connecter", "OK", "Non");
        }
    }
}