using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3.models
{
    class Gate:Belt
    {
        public int gateId { get; private set; }
        
        /// <summary>
        /// This is the constructor for the gate class.
        /// Since whe inherit from the Belt-class, we use base to get the point for this gate.
        /// </summary>
        /// <param name="gateID"></param>
        /// <param name="location"></param>
        public Gate(int gateID, Point location)
            : base(location)
        {
            this.gateId = gateID;
            this.point = new Point(location.X, location.Y);
        }
    }
}
