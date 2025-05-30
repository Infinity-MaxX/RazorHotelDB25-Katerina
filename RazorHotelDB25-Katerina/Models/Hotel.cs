﻿namespace RazorHotelDB25_Katerina.Models
{
    public class Hotel
    {
        #region Instances
        
        #endregion

        #region Properties
        public int HotelNr { get; set; }
        public String Navn { get; set; }
        public String Adresse { get; set; }
        #endregion

        #region Constructor
        public Hotel()
        {
        }

        public Hotel(int hotelNr, string navn, string adresse)
        {
            HotelNr = hotelNr;
            Navn = navn;
            Adresse = adresse;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{nameof(HotelNr)}: {HotelNr}, {nameof(Navn)}: {Navn}, {nameof(Adresse)}: {Adresse}";
        }
        #endregion
    }
}
