using System;
using System.Collections.Generic;
using Model;

namespace FourplacesApp
{
    public class PlacesList
    {
        public List<PlaceItemSummary> Liste { get; set; }
        public PlacesList()
        {
            Liste = new List<PlaceItemSummary>();
        }
        public PlacesList(List<PlaceItemSummary> liste)
        {
            Liste = liste;
        }

    }
}
