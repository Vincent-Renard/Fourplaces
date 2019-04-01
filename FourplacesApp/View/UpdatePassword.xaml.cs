using System;
using System.Collections.Generic;
using FourplacesApp.ViewModel;
using Storm.Mvvm.Forms;
using Xamarin.Forms;

namespace FourplacesApp
{
    public partial class UpdatePassword : BaseContentPage
    {
        public UpdatePassword()
        {
            InitializeComponent();
            BindingContext = new UpdatePasswordViewModel(Navigation);
        }
    }
}
