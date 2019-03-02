using System;
namespace FourplacesApp.Model
{
    public class Place
    {
        private int SmallIs = 32;

        public String ImgPlace
        {
            get;
            private set;
        }
        public String NomPlace
        {
            get;
            private set;
        }
        public String DescPlace
        {
            get;
            private set;
        }

        public Place(string nomPlace, string imgPlace, string descPlace)
        {
            ImgPlace = imgPlace;
            NomPlace = nomPlace;
            DescPlace = descPlace;
        }
        public string GetSmallDesc()
        {
            if (DescPlace.Length <= this.SmallIs) return DescPlace;
            else return DescPlace.Substring(0, this.SmallIs);
        }
    }
}
