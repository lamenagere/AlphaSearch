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
        public bool IsCaseSensitive { get; set; }

        public Node(char value, T details, int depth, Node<T> parent, bool isCaseSensitive = true)
        {
            Value = value;
            Depth = depth;
            Parent = parent;
            Children = new List<Node<T>>();
            Details = details;
            IsCaseSensitive = isCaseSensitive;
        }

        public static Node<T> CreateRootNode()
        {
            return new Node<T>('^', null, 0, null);
        }

        public Node<T> FindChildNode(char c)
        {
            foreach (var child in Children)
            {
                if (child.Value == c || (!IsCaseSensitive && char.ToLower(child.Value) == char.ToLower(c))) return child;
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
