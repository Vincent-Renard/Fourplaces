using System;
using System.Collections.Generic;
using FourplacesApp.ViewModel;
using Storm.Mvvm.Forms;
using Xamarin.Forms;

namespace FourplacesApp
{
    public partial class Sign : BaseContentPage
    {
        public Sign()
        {
            InitializeComponent();
            BindingContext = new SignViewModel(Navigation);
        }
    }
}
