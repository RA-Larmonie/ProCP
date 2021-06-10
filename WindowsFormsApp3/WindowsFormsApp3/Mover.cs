using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp3.model_v2;

namespace WindowsFormsApp3
{
    public class Mover
    {
        public List<Node> thePath = new List<Node>();
        Vector2 location;
        Vector2 velocity;
        public bool killIt = false;
        public bool onPath = true;
        int counter = 0;
        Brush brush;

        public Mover(List<Node> path, string color)
        {
            this.thePath = path;
            if (color == "Blue") { this.brush = Brushes.Blue;  }
            else if (color == "Yellow") { this.brush = Brushes.Yellow; }
            else if (color == "Purple") { this.brush = Brushes.Purple; }
            else if (color == "Pink") { this.brush = Brushes.Pink; }
            else if (color == "SkyBlue") { this.brush = Brushes.Cyan; }
            else if (color == "Orange") { this.brush = Brushes.Orange; }


            location = new Vector2(thePath[0].Position.X * 50 + 15, thePath[0].Position.Y * 50 + 15);
        }

        public void Update()
        {
            if(location.Y == thePath[counter].Position.Y * 50 + 15 && location.X == thePath[counter].Position.X * 50 + 15)
            {
                if (counter == thePath.Count - 1)
                {
                    killIt = true;
                }
                else if (thePath[counter + 1].Position.X < thePath[counter].Position.X)
                {
                    velocity = new Vector2(-1, 0);
                    counter++;
                }
                else if (thePath[counter + 1].Position.X > thePath[counter].Position.X)
                {
                    velocity = new Vector2(1, 0);
                    counter++;
                }
                else if (thePath[counter + 1].Position.Y > thePath[counter].Position.Y)
                {
                    velocity = new Vector2(0, 1);
                    counter++;
                }
                else if (thePath[counter + 1].Position.Y < thePath[counter].Position.Y)
                {
                    velocity = new Vector2(0, -1);
                    counter++;
                }
            }

            location = Vector2.Add(location, velocity);
        }

        public void Display(Graphics e)
        {
            if (killIt)
            {
                e.FillEllipse(Brushes.Transparent, location.X, location.Y, 20, 20);
                onPath = false;
            }
            else
            {
                e.FillEllipse(brush, location.X, location.Y, 20, 20);
            }
            
        }

    }
}
