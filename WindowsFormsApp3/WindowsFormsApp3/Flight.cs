using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    [Serializable]
    public class Flight
    {
        public string FlightNr { get; set; }
        public int NrOffBaggages { get; set; }
        public string Color { get; set; }

        public int NrOnStart { get; set; }
        public int NrOnPath { get; set; }
        public int NrOnEnd { get; set; }


        public Flight(string flight, int nrOffBaggages, string color)
        {
            this.FlightNr = flight;
            this.NrOffBaggages = nrOffBaggages;            
            this.Color = color;
            
        }

        override
        public string ToString()
        {
            return "FlightNr: " + FlightNr + ", Nr of Luggages: " + NrOffBaggages + ", Color: " + Color;
        }
    }
}
