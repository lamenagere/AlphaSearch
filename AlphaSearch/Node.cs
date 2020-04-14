using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace AlphaSearch
{
    public class Node<T> where T : class
    {
        public char Value { get; set; }
        public List<Node<T>> Children { get; set; }
        public Node<T> Parent { get; set; }
        public int Depth { get; set; }
        public bool IsLeaf => Children.Count == 0;
        public T Details { get; set; }

        public Node(char value, T details, int depth, Node<T> parent)
        {
            Value = value;
            Depth = depth;
            Parent = parent;
            Children = new List<Node<T>>();
            Details = details;
        }

        public static Node<T> CreateRootNode()
        {
            return new Node<T>('^', null, 0, null);
        }

        public Node<T> FindChildNode(char c)
        {
            foreach (var child in Children)
            {
                if (child.Value == c) return child;
            }
            return null;
        }
        
        public void DeleteChildNode(char c)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i].Value == c)
                {
                    Children.RemoveAt(i);
                }
            }
        }
    }
}
