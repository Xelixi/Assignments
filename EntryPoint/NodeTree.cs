using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace EntryPoint
{
    class NodeTree
    {
        private Node rootNode = null;
        private List<Vector2> nodeList = new List<Vector2>();

        public NodeTree(Vector2[] vectorArray)
        {
            if (vectorArray != null)
            {
                foreach (Vector2 vectors in vectorArray)
                    insertIntoTree(vectors);
            }
        }

        public void insertIntoTree(Vector2 vector2)
        {
            Node current = rootNode;
            Node previous = null;
            bool boolean = false;
            bool leftTree = false;

            while (current != null)
            {
                previous = current;
                if (boolean)
                {
                    if (leftTree = vector2.X < current.Vec2.X)
                    {
                        current = current.Left;
                    }
                    else
                    {
                        current = current.Right;
                    }
                }

                else
                {
                    if (leftTree = vector2.Y < current.Vec2.Y)
                    {
                        current = current.Left;
                    }
                    else
                    {
                        current = current.Right;
                    }
                }

                boolean = !boolean;
            }
            if (rootNode == null)
            {
                rootNode = new Node(vector2);
            }
            else
            {
                if (leftTree)
                {
                    previous.Left = new Node(vector2);
                }
                else
                {
                    previous.Right = new Node(vector2);
                }
            }
        }

        public static double calcDistance(Vector2 specialBuildingVector, Vector2 house)
        {
            return Math.Sqrt(Math.Pow(house.X - specialBuildingVector.X, 2) + Math.Pow(house.Y - specialBuildingVector.Y, 2));
        }

        public bool inRange(Tuple<Vector2, float> house, Node node)
        {
            double range = calcDistance(house.Item1, node.Vec2);
            if (range < house.Item2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Vector2> traverseRecursive(Node node, Tuple<Vector2, float> listHouseDistances)
        {
            if (node != null)
            {
                traverseRecursive(node.Left, listHouseDistances);
                if (inRange(listHouseDistances, node))
                {
                    nodeList.Add(node.Vec2);
                }
                traverseRecursive(node.Right, listHouseDistances);
                if (inRange(listHouseDistances, node))
                {
                    nodeList.Add(node.Vec2);
                }
            }
            return nodeList;
        }

        public IEnumerable<IEnumerable<Vector2>> Traverse(NodeTree root, IEnumerable<Tuple<Vector2, float>> housesAndDistances)
        {
            IEnumerable<IEnumerable<Vector2>> specialNodesArr;
            var specialNodes = new List<List<Vector2>>();
            List<Vector2> nodes = new List<Vector2>();
            if (root != null)
            {
                foreach (var item in housesAndDistances)
                {
                    nodes = traverseRecursive(root.rootNode, item);

                    specialNodes.Add(nodes);
                }
            }
            else
            {
                specialNodesArr = specialNodes.ToArray();
                return specialNodesArr;
            }
            specialNodesArr = specialNodes.ToArray();
            return specialNodesArr;
        }

    }
}







//    {
//        private Node _rootNode = null;
//        public NodeTree(Vector2[] vec2)
//        {
//            if (vec2 != null)
//            {
//                foreach (Vector2 v in vec2)
//                    Insert(v);
//            }
//        }

//        public void Insert(Vector2 vec2)
//        {
//            Node current = _rootNode;
//            Node previous = null;
//            bool useX = false;
//            bool useLeftSubtree = false;

//            while (current != null)
//            {

//                previous = current;
//                if (useX)
//                {
//                    if (useLeftSubtree = vec2.X < current.Vec2.X)
//                    {
//                        current = current.Left;
//                    }
//                    else
//                    {
//                        current = current.Right;
//                    }
//                }
//                else
//                {
//                    if (useLeftSubtree = vec2.Y < current.Vec2.Y)
//                        current = current.Left;
//                    else
//                        current = current.Right;
//                }
//                useX = !useX;

//            }

//            if (_rootNode == null)
//                _rootNode = new Node(vec2);
//            else
//            {
//                if (useLeftSubtree)
//                    previous.Left = new Node(vec2);
//                else
//                    previous.Right = new Node(vec2);
//            }
//        }

//    }
//}