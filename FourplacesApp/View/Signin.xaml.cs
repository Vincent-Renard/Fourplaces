using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FourplacesApp.View
{
    public partial class Signin : ContentPage
    {



        public string _Email { get; set; }
        public string _FirstName { get; set; }
        public string _LastName { get; set; }
        public string _Password { get; set; }

        public Signin()
        {
            InitializeComponent();
        }



    }
}
