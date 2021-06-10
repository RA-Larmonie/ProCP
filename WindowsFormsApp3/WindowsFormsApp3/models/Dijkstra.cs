using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3.models
{
    class Dijkstra
    {

        private List<Belt> vertices;
        private List<Belt> shortestPaths;

        public Dijkstra(Belt[] vertices, int[][] graph, int source)
        {
            this.vertices = new List<Belt>();
            InitializeSource(vertices, vertices[source]);
            // suppose we can go from A to B and from A to C, 
            //in this case we should give perioty to to the one with the lower weighted
            PriorityQueue<Belt> queues = new PriorityQueue<Belt>();
            for (int i = 0; i < vertices.Length; i++)
            {
                // add/ push the vertices and it weight
                queues.Enqueue(vertices[i], vertices[i].Weight);
            }
            // triversing all other vertices
            while (queues.Count > 0)
            {
                Belt u = queues.Dequeue();
                this.vertices.Add(u);
                //again traversing to all vertices
                for (int v = 0; v < graph[Convert.ToInt32(u.Name)].Length; v++)
                {
                    if (graph[Convert.ToInt32(u.Name)][v] > 0)
                    {
                        Relax(u, vertices[v], graph[Convert.ToInt32(u.Name)][v]);
                        //updating priority value since distance is changed
                        queues.UpdatePriority(vertices[v], vertices[v].Weight);
                    }
                }
            }
        }


        private void ContiniouslyFindShortestPath(Belt startingBelt, Belt endingBelt)
        {
            try
            {
                this.shortestPaths = new List<Belt>();
                if (endingBelt != startingBelt)
                {
                    if (endingBelt == null)
                    {
                        throw new Exception("end belt is null--> test");
                    }
                    GetShortestPath(startingBelt, endingBelt.Parent);
                    shortestPaths.Add(endingBelt);
                    Console.WriteLine("Vertex {0} weight: {1}", endingBelt.Name, endingBelt.Weight);
                }
                else
                {
                    shortestPaths.Add(endingBelt);
                    Console.WriteLine("Vertex {0} weight: {1}", endingBelt.Name, endingBelt.Weight);
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Please complete path from check-in " + startingBelt + "until departure gate " + startingBelt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<Belt> GetShortestPath(Belt startingBelt, Belt endingBelt)
        {
            try
            {
                if (endingBelt == null)
                {
                    shortestPaths = new List<Belt>();
                    throw new Exception("end Belt is null-->test");
                }
                else
                {
                    ContiniouslyFindShortestPath(startingBelt, endingBelt);
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }

            catch (Exception ex)
            {
                shortestPaths = null;
                Console.WriteLine(ex.Message);
            }
            return shortestPaths;
        }

        public void Relax(Belt u, Belt v, int weight)
        {
            if (v.Weight > u.Weight + weight)
            {
                v.Weight = u.Weight + weight;
                v.Parent = u;
            }
        }

        /* we need to initialize the source to do that we should consider the following
           we need lsit of vertics of type belt becuase belt has got the vertices
           start point/ vertics
         ---------------------------------------------------
         vetices|        wieght   |       Prevoius vertoice
         ---------------------------------------------------
              A |             0   |           parent ==null
              B |             5   |           parent = A
              C |             2   |           parent = A

          */
        public void InitializeSource(Belt[] vertices, Belt startVertice)
        {
            //loop throught he lsit of given vertices
            foreach (Belt v in vertices)
            {
                //get assign the weight--> initially the wight of all vertices ins infinity
                v.Weight = int.MaxValue;
                // parent  a --> a you remain in the a
                v.Parent = null;
            }
            // the wright of  starting Vertice is nothing but 0 as per the Dijkstra algor
            startVertice.Weight = 0;

        }

        

    }



}
