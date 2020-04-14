using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace AlphaSearch
{
    public class Node
    {
        public char Value { get; set; }
        public List<Node> Children { get; set; }
        public Node Parent { get; set; }
        public int Depth { get; set; }
        public bool IsLeaf => Children.Count == 0;

        public Node(char value, int depth, Node parent)
        {
            Value = value;
            Depth = depth;
            Parent = parent;
            Children = new List<Node>();
        }

        public Node FindChildNode(char c)
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
