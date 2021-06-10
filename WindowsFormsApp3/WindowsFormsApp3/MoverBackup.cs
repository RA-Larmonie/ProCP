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
    class MoverBackup
    {
        public List<Node> thePath = new List<Node>();
        Vector2 location;
        Vector2 location2;
        Vector2 velocity;
        public bool killIt = false;
        int counter = 1;

        public MoverBackup(List<Node> path)
        {
            thePath = path;
            location = new Vector2(thePath[0].Position.X * 50 + 15, thePath[0].Position.Y * 50 + 15);
            location2 = new Vector2(thePath[1].Position.X * 50 + 15, thePath[1].Position.Y * 50 + 15);
            velocity = Vector2.Subtract(location2, location);
            if (velocity.X > 0)
            {
                velocity = new Vector2(1, 0);
            }
            else if (velocity.X < 0)
            {
                velocity = new Vector2(-1, 0);
            }
            else if (velocity.Y > 0)
            {
                velocity = new Vector2(0, 1);
            }
            else if (velocity.Y < 0)
            {
                velocity = new Vector2(0, -1);
            }
        }

        public void Update()
        {
            if (counter <= thePath.Count - 1)
            {
                if (location.Y == thePath[thePath.Count - 1].Position.Y * 50 + 15 && location.X == thePath[thePath.Count - 1].Position.X * 50 + 15)
                {
                    killIt = true;
                    Console.WriteLine("STOP!");

                }
                else if (counter < thePath.Count - 2)
                {
                    if (location.Y == thePath[counter].Position.Y * 50 + 15 && location.X == thePath[counter].Position.X * 50 + 15)
                    {
                        if (thePath[counter + 1].Position.Y > thePath[counter].Position.Y)
                        {
                            velocity = new Vector2(0, 1);
                            counter++;
                        }
                        if (thePath[counter + 1].Position.Y < thePath[counter].Position.Y)
                        {
                            velocity = new Vector2(0, -1);
                            counter++;
                        }
                        if (thePath[counter + 1].Position.X > thePath[counter].Position.X)
                        {
                            velocity = new Vector2(1, 0);
                            counter++;
                        }
                        if (thePath[counter + 1].Position.X < thePath[counter].Position.X)
                        {
                            velocity = new Vector2(-1, 0);
                            counter++;
                        }

                    }
                }
            }

            location = Vector2.Add(location, velocity);


        }

        public void Display(Graphics e)
        {
            if (killIt)
            {
                e.FillEllipse(Brushes.Transparent, location.X, location.Y, 20, 20);
            }
            else
            {
                e.FillEllipse(Brushes.Blue, location.X, location.Y, 20, 20);
            }

        }
        //enum type/color


    }
}
