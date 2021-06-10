using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3.models
{
    class PriorityQueue<T>
    {
        List<Node> queues = new List<Node>();

        int heapSize = 1;
        private bool isMinPriorityQ = true;
        public int Count
        {
            get { return queues.Count; }
        }

        public T Dequeue()
        {
            if (queues.Any())
            {
                var itemTobeRemoved = queues[0].Obj;
                queues[0] = queues[heapSize];
                queues.RemoveAt(heapSize);
                heapSize--;
                return itemTobeRemoved;
            }
            else
            {
                throw new Exception("There is nothing in the Queue, Queue is Emplty");
            }
        }

        public void Enqueue(T obj, int priorty)
        {
            var node = new Node() { Priority = priorty, Obj = obj };
            // push/add the Node into the list of Nodes
            queues.Add(node);
            heapSize++;
            if (isMinPriorityQ)
                BuildHeapMin(heapSize);
            else
                BuildHeapMax(heapSize);
        }
        public bool IsInQueue(T obj)
        {
            foreach (Node n in queues) { return Object.ReferenceEquals(n.Obj, obj); }
            return false;
        }

        public void BuildHeapMax(int i)
        {
            while (i > 0 && queues[(i - 1) / 2].Priority < queues[i].Priority)
            {
                Swap(i, (i - 1) / 2);
                i = i - 1 / 2;
            }
        }

        public void BuildHeapMin(int i)
        {
            while (i > 0 && queues[(i - 1) / 2].Priority > queues[i].Priority)
            {
                Swap(i, (i - 1) / 2);
                i = i - 1 / 2;
            }
        }
        private void Swap(int a, int b)
        {
            var temp = queues[a];
            queues[a] = queues[b];
            queues[b] = temp;
        }

        public void UpdatePriority(T obj, int priority)
        {
            int i = 0;
            for (; i <= heapSize; i++)
            {
                Node node = queues[i];
                if (object.ReferenceEquals(node.Obj, obj))
                {
                    node.Priority = priority;
                    if (isMinPriorityQ)
                    {
                        BuildHeapMin(i);
                        MinHeapify(i);
                    }
                    else
                    {
                        BuildHeapMax(i);
                        MaxHeapify(i);
                    }
                }
            }
        }
        /// <summary>
        /// A max-heap is a complete binary tree in which the value in each internal node is greater than or equal 
        /// to the values in the children of that node. Mapping the elements of a heap into an array 
        /// is trivial: if a node is stored a index k, then its left child is stored at index 
        /// 2k+1 and its right child at index 2k+2.
        /// for eaxple
        /// A Max Heap is a Complete Binary Tree. A Max heap is typically represented as an array. The root element will be at Arr[0]. Below table shows indexes of other nodes for the ith node, i.e., Arr[i]:
        ///  Arr[(i - 1) / 2] Returns the parent node.
        ///  Arr[(2 * i) + 1] Returns the left child node.
        ///      Arr[(2 * i)+2] Returns the right child node.
        /// </summary>
        /// <param name="i"></param>
        /// private int ChildL(int i)



        private void MaxHeapify(int i)
        {
            int left = ChildL(i);
            int right = ChildR(i);

            int heighst = i;

            if (left <= heapSize && queues[heighst].Priority < queues[left].Priority)
                heighst = left;
            if (right <= heapSize && queues[heighst].Priority < queues[right].Priority)
                heighst = right;

            if (heighst != i)
            {
                Swap(heighst, i);
                MaxHeapify(heighst);
            }
        }
        private void MinHeapify(int i)
        {
            int left = ChildL(i);
            int right = ChildR(i);

            int lowest = i;

            if (left <= heapSize && queues[lowest].Priority > queues[left].Priority)
                lowest = left;
            if (right <= heapSize && queues[lowest].Priority > queues[right].Priority)
                lowest = right;

            if (lowest != i)
            {
                Swap(lowest, i);
                MinHeapify(lowest);
            }
        }

        private int ChildL(int i)
        {
            return i * 2 + 1;
        }
        private int ChildR(int i)
        {
            return i * 2 + 2;
        }

        class Node
        {
            public int Priority { get; set; }
            public T Obj { get; set; }
        }
    }
}
