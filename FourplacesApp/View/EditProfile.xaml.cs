using System;
using System.Collections.Generic;
using Storm.Mvvm;
using FourplacesApp.ViewModel;
using Xamarin.Forms;
using Storm.Mvvm.Forms;

namespace FourplacesApp
{
    public partial class EditProfile : BaseContentPage
    {
        public EditProfile()
        {
            InitializeComponent();
            BindingContext = new EditProfileViewModel(Navigation);
        }
    }
}
