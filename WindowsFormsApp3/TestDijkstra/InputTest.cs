using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsApp3;

namespace TestDijkstra
{

    [TestClass]
    public class InputTest
    {
        [TestMethod]
        public void addFlight_AddFlightsToList_returnBooleanValue() 
        {
            Flight fly = new Flight("TK-20",20,"Pink");

            Airport airport = new Airport();
            //List<Flight> flights_expected = new List<Flight>();
            bool check = airport.addFlight(fly);

            Assert.IsTrue(check);

        }

        [TestMethod]
        public void getNumberOfLuggages_getNrOfLuggagesForEachFlight_returnInteger() 
        {
            

            Flight fly = new Flight("TK-20", 20, "Pink");

            Airport airport = new Airport();
            List<Flight> flights_expected = new List<Flight>();
            airport.addFlight(fly);


            Assert.AreEqual(20, airport.GetNrOfLuggages());
            
        }

        [TestMethod]
        public void getNumberOfFlights_getNrOfFlightsFromList_returnInteger()
        {
            Flight fly = new Flight("TK-20", 20, "Pink");
            Flight fly2 = new Flight("SYR-01", 40, "Pink");
            Flight fly3 = new Flight("ABN-40", 60, "Pink");

            Airport airport = new Airport();
            List<Flight> flights_expected = new List<Flight>();
            airport.addFlight(fly);
            airport.addFlight(fly2);
            airport.addFlight(fly3);

            Assert.AreEqual(3, airport.GetNrOfFlights());
        }

    }
}
