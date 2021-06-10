using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsApp3.model_v2;



namespace UnitTest
{
    [TestClass]
    public class DijkstraTests
    {

        //[TestMethod]
        //public void FindPath_FindShortestPathFromAToB_ReturnsListOfNodes()
        //{

        //    Node node_start = new Node(new Coordinate(0, 4), Node.NodeType.start);
        //    Node node_end = new Node(new Coordinate(3, 5), Node.NodeType.end);
           
           

        //    Node[,] grid = new Node[33, 19];
        //    NewDijkstra dijkstra = new NewDijkstra(grid);

        //    List<Node> expected = new List<Node>();

        //    expected.Add(new Node(new Coordinate(1, 4), Node.NodeType.free));
        //    expected[0].Parent = node_start;
        //    expected[0].Cost = 2;
        //    expected[0].DistanceToTarget = 3;

        //    expected.Add(new Node(new Coordinate(2, 4), Node.NodeType.free));
        //    expected[1].Parent = expected[0];
        //    expected[1].Cost = 3;
        //    expected[1].DistanceToTarget = 2;

        //    expected.Add(new Node(new Coordinate(3, 4), Node.NodeType.free));
        //    expected[2].Parent = expected[2];
        //    expected[2].Cost = 4;
        //    expected[2].DistanceToTarget = 1;

        //    List<Node> actual = dijkstra.FindPath(node_start,node_end);
            
        //    CollectionAssert.AreEquivalent(expected, actual);

        //}

        [TestMethod]
        public void FindPath_GetListOfAdjacentNodes_ReturnsListOfNodes()
        {
            //Coordinate coord = new Coordinate(0, 4); //int a, b
            Node node_A = new Node(new Coordinate(0, 4), Node.NodeType.start);
            Node[,] grid = new Node[33, 19];
            NewDijkstra dijkstra = new NewDijkstra(grid);

            List<Node> expected = new List<Node>();
            expected.Add(grid[1, 4]/*(new Coordinate(1, 4), Node.NodeType.free)*/);
            expected.Add(grid[0, 3]/*(new Coordinate(0, 3), Node.NodeType.free)*/);
            expected.Add(grid[0, 5]/*(new Coordinate(0, 5), Node.NodeType.free)*/);

            List<Node> listAdjecentNode = dijkstra.GetAdjacentNodes(node_A);

            CollectionAssert.AreEquivalent(expected, listAdjecentNode);
        }
    }
}
