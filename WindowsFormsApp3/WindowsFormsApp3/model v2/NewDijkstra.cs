using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace WindowsFormsApp3.model_v2
{
    public class NewDijkstra
    {
        Node[,] Grid;


        public NewDijkstra(Node[,] grid)
        {
            Grid = grid;
        }


        public List<Node> FindPath(Node Start, Node End)
        {
            Node start = Start;
            Node end = End;

            List<Node> Path = new List<Node>();
            List<Node> OpenList = new List<Node>();
            List<Node> ClosedList = new List<Node>();
            List<Node> adjacencies;
            Node current = start;

            // add start node to Open List
            OpenList.Add(start);

            while (OpenList.Count != 0 && !ClosedList.Exists(x => x.Position == end.Position))
            {
                current = OpenList[0];
                OpenList.Remove(current);
                ClosedList.Add(current);
                adjacencies = GetAdjacentNodes(current);

                foreach (Node n in adjacencies)
                {
                    if (!ClosedList.Contains(n) && n.Type != Node.NodeType.obs && n.Type != Node.NodeType.path)
                    {
                        if (!OpenList.Contains(n))
                        {
                            if(n.Type == Node.NodeType.start)
                            {
                                if(n.Position.X != start.Position.X && n.Position.Y != start.Position.Y)
                                {

                                }
                                else {
                                    n.Parent = current;
                                    n.DistanceToTarget = Math.Abs(n.Position.X - end.Position.X) + Math.Abs(n.Position.Y - end.Position.Y);
                                    n.Cost = n.Weight + n.Parent.Cost;
                                    OpenList.Add(n);
                                    OpenList = OpenList.OrderBy(node => node.F).ToList<Node>();
                                }
                            }
                            else if (n.Type == Node.NodeType.end)
                            {
                                if (n.Position.X != end.Position.X && n.Position.Y != end.Position.Y)
                                {

                                }
                                else
                                {
                                    n.Parent = current;
                                    n.DistanceToTarget = Math.Abs(n.Position.X - end.Position.X) + Math.Abs(n.Position.Y - end.Position.Y);
                                    n.Cost = n.Weight + n.Parent.Cost;
                                    OpenList.Add(n);
                                    OpenList = OpenList.OrderBy(node => node.F).ToList<Node>();
                                }
                            }
                            else
                            {
                                n.Parent = current;
                                n.DistanceToTarget = Math.Abs(n.Position.X - end.Position.X) + Math.Abs(n.Position.Y - end.Position.Y);
                                n.Cost = n.Weight + n.Parent.Cost;
                                OpenList.Add(n);
                                OpenList = OpenList.OrderBy(node => node.F).ToList<Node>();
                            }
                            
                        }
                    }
                }
            }

            // construct path, if end was not closed return null
            if (!ClosedList.Exists(x => x.Position == end.Position))
            {
                return null;
            }

            // if all good, return path
            Node temp = ClosedList[ClosedList.IndexOf(current)];
            if (temp == null) return null;
            do
            {
                Path.Add(temp);
                temp = temp.Parent;
            } while (temp != start && temp != null );
            Path.RemoveAt(0);
            Path.Insert(0, start);  //Extra changes that were added for the purpose of displayig the whole path in the case importing/ exporting it being that we would assign the start & end node immediately in the path
            Path.Add(end);          //Extra changes that were added
            return Path;
        }

        //public List<Node> temp = new List<Node>();

        public List<Node> GetAdjacentNodes(Node n)
        {
            List<Node> temp = new List<Node>();
            int x = (int)n.Position.X;
            int y = (int)n.Position.Y;
            
            if (x + 1 < Grid.GetLength(0))
            {
                temp.Add(Grid[x + 1, y]);
            }
            if (x - 1 >= 0)
            {
                temp.Add(Grid[x - 1, y]);
            }
            if (y - 1 >= 0)
            {
                temp.Add(Grid[x, y - 1]);
            }
            if (y + 1 < Grid.GetLength(1))
            {
                temp.Add(Grid[x, y +1]);
            }
            return temp;
        }
    }
}

