using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    [Serializable]
    public class Airport
    {        
        public List<Flight> flights { get; }
        private List<Employee> users;

        public Airport()
        {
            dummyEmpData();
            flights = new List<Flight>();
        }



        public void dummyEmpData()
        {
            users = new List<Employee>();
            users.Add(new Employee(12, "Fadi", "Fadi", "123123", Employee.RoleType.Admin));
            users.Add(new Employee(11, "Alex", "Alex", "123123", Employee.RoleType.Employee));
           
        }

        public List<Employee> getUsers()
        {
            return this.users;
        }

        public void deleteUser(Employee tempUser)
        {
            users.Remove(tempUser);
        }

        public void addUser(Employee u)
        {
            users.Add(u);
        }

        public void updateAccess(Employee user, Employee.RoleType access)
        {
            foreach(Employee u in users)
            {
                if(u.Name == user.Name)
                {
                    u.Role = access;
                }
            }
        }





        public bool addFlight(Flight flight)
        {
            bool check = true;
            if (flights.Count > 0) 
            {
                
                foreach (Flight check_flight in flights)
                {
                    if (flight.FlightNr == check_flight.FlightNr)
                    {
                        check = false;
                        check_flight.NrOffBaggages = flight.NrOffBaggages;                        
                        break;
                    }                   
                }
                if(check)
                {
                    flights.Add(flight);
                }
            }
            else 
            {
                flights.Add(flight);
            }
            return check;

        }

        public List<Flight> GetFlights() 
        {
            return flights;
        }

        public int GetNrOfLuggages() 
        {
            int tempLuggage =0;

            foreach (Flight luggage in flights)
            {
                 tempLuggage += luggage.NrOffBaggages ;

            }
            return tempLuggage;
        }

        public int GetNrOfFlights() 
        {
            int tempFlights = 0;
            tempFlights= flights.Count();
            return tempFlights;
        }

        override
        public string ToString()
        {
            return ";";
        }        
    }
}
