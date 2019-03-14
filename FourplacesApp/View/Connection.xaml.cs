using System;
using System.Collections.Generic;
using Storm.Mvvm.Forms;
using Xamarin.Forms;

namespace FourplacesApp
{
    public partial class Connection : BaseContentPage
    {
        //[XamlCompilation(XamlCompilationOptions.Compile)]
        public Connection()
        {
            InitializeComponent();
            BindingContext = new ConnectionViewModel(Navigation);
        }
    }
}
