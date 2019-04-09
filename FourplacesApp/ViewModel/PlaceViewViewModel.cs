using System;

using Xamarin.Forms;

namespace FourplacesApp.ViewModel
{
    public class PaceViewViewModel : ContentPage
    {
        public PaceViewViewModel()
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

