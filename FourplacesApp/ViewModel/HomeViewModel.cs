using System;

using Xamarin.Forms;

namespace FourplacesApp.ViewModel
{
    public class HomeViewModel : ContentPage
    {
        public HomeViewModel()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

