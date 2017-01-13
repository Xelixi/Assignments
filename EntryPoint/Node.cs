using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace EntryPoint
{
    class Node
    {
        private Vector2 vector2;
        private Node left;
        private Node right;

        public Node(Vector2 vec2)
        {
            vector2 = vec2;
        }

        public Vector2 Vec2
        {
            get { return vector2; }
            set { vector2 = value; }
        }

        public Node Right
        {
            get { return right; }
            set { right = value; }
        }

        public Node Left
        {
            get { return left; }
            set { left = value; }
        }
    }
}