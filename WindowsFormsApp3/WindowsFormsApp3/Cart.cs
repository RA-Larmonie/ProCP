using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    public class Cart
    {
        public int Capcity { get; }
        private int id;

        public Cart(int id, int capacity)
        {
            this.id = id;
            this.Capcity = capacity;
        }

        override
        public string ToString()
        {
            return "CartID: " + this.id + "    ;  Capacity: " + this.Capcity;
        }
    }
}
