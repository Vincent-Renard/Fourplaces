using System;
using System.Collections.Generic;
using Model.Dtos;
using Storm.Mvvm;
using Xamarin.Forms;

namespace FourplacesApp.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private INavigation Navigation;
        private string _titlePlace;
        private string _description;
        private List<PlaceItemSummary> _liste;

        public string DescriptionPlace
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
            public string TitlePlace
        {
            get => _titlePlace;
            set => SetProperty(ref _titlePlace, value);
        }
        public List<PlaceItemSummary> Liste
        {
            get => _liste;
            set => SetProperty(ref _liste, value);
        }

        public HomeViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }
    }
}

