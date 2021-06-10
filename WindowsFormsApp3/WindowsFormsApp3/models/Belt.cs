

using System.Windows;
using System.Drawing.Drawing2D;




namespace WindowsFormsApp3.models
{
    class Belt
    {
        public int Weight { get; set; }
        public Belt Parent { get; set; }
        public string Name { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int BeltId { get; set; }
        public Point point;
      
        public Belt(Point location)
        {
            this.point = new Point(location.X, location.Y);
        }

        public Belt()
        {
        }
    }




   
}
