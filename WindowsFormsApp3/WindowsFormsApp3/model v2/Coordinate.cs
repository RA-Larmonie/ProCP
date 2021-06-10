using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3.model_v2
{
    [Serializable]
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int i, int j)
        {
            this.X = i;
            this.Y = j;
        }        
    }
}
