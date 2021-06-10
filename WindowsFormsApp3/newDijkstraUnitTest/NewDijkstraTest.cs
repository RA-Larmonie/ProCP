using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WindowsFormsApp3;
using WindowsFormsApp3.model_v2;

namespace newDijkstraUnitTest
{
    [TestClass]
    public class NewDijkstraTest
    {
        /// <summary>
        /// Unit test for finding the shortest path.
        /// Using the FindPath method from model v2 folder.
        /// Returns a list of node from end {excluding the end} upto and including the start.
        /// </summary>
       /* [TestMethod]
        public void FindaPath_FindShortestPathFromAToB_ReturnListOfNodes()
        {
            //Arrange   initialze objects            
            Node start;
            Node end;
            
            Node[,] grid = new Node[33, 19];            
            NewDijkstra dijkstra = new NewDijkstra(grid);
            start = grid[0, 4];
            end = grid[3, 5];
            start.Type = Node.NodeType.start;
            end.Type = Node.NodeType.end;
            List<Node> expected = new List<Node>();

            //Act
            grid[1, 4].DistanceToTarget = 3;
            grid[1, 4].Cost = 2;
            grid[1, 4].Parent = start;
            grid[1, 4].Type = Node.NodeType.free;

            grid[2, 4].DistanceToTarget = 2;
            grid[2, 4].Cost = 3;
            grid[2, 4].Parent = grid[1, 4];
            grid[2, 4].Type = Node.NodeType.free;

            grid[3, 4].DistanceToTarget = 1;
            grid[3, 4].Cost = 4;
            grid[3, 4].Parent = grid[2, 4];
            grid[3, 4].Type = Node.NodeType.free;

            /*Node a1 = new Node(new Coordinate(3, 4), Node.NodeType.free);
            Node b1 = new Node(new Coordinate(2, 4), Node.NodeType.free);
            Node c1 = new Node(new Coordinate(1, 4), Node.NodeType.free);

            c1.Cost = 2;
            c1.DistanceToTarget = 3;
            c1.Parent = start;

            b1.Cost = 3;
            b1.DistanceToTarget = 2;
            b1.Parent = c1;

            a1.Cost = 4;
            a1.DistanceToTarget = 1;
            a1.Parent = b1;*/
            /*
            Node a = grid[3, 4];
            Node b = grid[2, 4];
            Node c = grid[1, 4];

            expected.Add(a);
            expected.Add(b);
            expected.Add(c);

            List <Node> shortestPath = dijkstra.FindPath(start, end);

            //Assert
            CollectionAssert.AreEquivalent(expected, shortestPath);
        }*/

        /// <summary>
        /// Unit test for the NewDijkstra class.
        /// GetAdjacentNodes method.
        /// Returns list of Node. More specific 2 dimentional node
        /// </summary>
        [TestMethod]
        public void FindaPath_GetListOfAdjecentNodesOfSpecifiedNode_ReturnListOfNodes()
        {
            //Arrange   initialze objects            
            Node node_A = new Node(new Coordinate(0, 4), Node.NodeType.start);
            Node[,] grid = new Node[33, 19];
            NewDijkstra dijkstra = new NewDijkstra(grid);
            List<Node> expected = new List<Node>();


            //Act
            expected.Add(grid[1, 4]/*(new Coordinate(1, 4), Node.NodeType.free)*/);
            expected.Add(grid[0, 3]/*(new Coordinate(0, 3), Node.NodeType.free)*/);
            expected.Add(grid[0, 5]/*(new Coordinate(0, 5), Node.NodeType.free)*/);

            List<Node> listAdjecentNode = dijkstra.GetAdjacentNodes(node_A);

            //Assert
            CollectionAssert.AreEquivalent(expected, listAdjecentNode);
        }
    }
}
