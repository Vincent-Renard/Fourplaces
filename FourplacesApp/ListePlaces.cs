using System;

using Xamarin.Forms;

namespace FourplacesApp
{
    public class ListePlaces : ContentPage
    {
        public ListePlaces()
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

