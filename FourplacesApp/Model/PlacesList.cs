using System;
using System.Collections.Generic;
using Model.Dtos;

namespace FourplacesApp
{
    public class PlacesList
    {
        public List<PlaceItemSummary> Liste { get; set; }
        public PlacesList()
        {
        }
        public PlacesList(List<PlaceItemSummary> liste)
        {
            Liste = liste;
        }

    }
}
