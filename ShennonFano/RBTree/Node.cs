using System;
using System.Collections.Generic;
using System.Text;

namespace Array_Csharp_1laba
{
    public enum Color
    {
        black,
        red
    }

    public class Node<KEY, DATA>
    {
        public DATA Data;

        public KEY Key;

        public Color Color { get; set; }

        public Node<KEY, DATA> Left { get; set; }

        public Node<KEY, DATA> Right { get; set; }

        public Node<KEY, DATA> Parent { get; set; }

        public Node(KEY Key, DATA Data)
        {
            this.Data = Data;

            this.Key = Key;

            Color = Color.red;

            Parent = null;

            Left = null;

            Right = null;

        }

        public Node()
        { 
        
        
        }

        public static bool operator ==(Node<KEY, DATA> node, Color color)
        {
            if (node == null)
            {
                if (color == Color.black)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return node.Color == color;

        }

        public static bool operator !=(Node<KEY, DATA> node, Color colorNode2)
        {
            return !(node == colorNode2);
        }

    }



}
