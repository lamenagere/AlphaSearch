using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaSearch
{
    public class Trie
    {
        public bool AllowStoreDuplicates { get; protected set; }
        public bool AllowReturnDuplicates { get; protected set; }

        private readonly Node _root;

        public Trie(bool allowStoreDuplicates, bool allowReturnDuplicates)
        {
            _root = new Node('^', 0, null);
            AllowStoreDuplicates = allowStoreDuplicates;
            AllowReturnDuplicates = allowReturnDuplicates;
        }

        private Node Prefix(string s)
        {
            var currentNode = _root;
            var result = currentNode;

            foreach (char c in s)
            {
                currentNode = currentNode.FindChildNode(c);
                if (currentNode == null) break;

                result = currentNode;
            }

            return result;
        }

        private bool Search(string s)
        {
            Node prefix = Prefix(s);
            return prefix.Depth == s.Length && prefix.FindChildNode('$') != null;
        }

        public void InsertRange(List<string> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                Insert(items[i]);
            }
        }

        public void Insert(string s)
        {
            Node commonPrefix = Prefix(s);
            if (AllowStoreDuplicates == false && commonPrefix.Depth == s.Length) return;

            Node current = commonPrefix;

            for (int i = current.Depth; i < s.Length; i++)
            {
                Node newNode = new Node(s[i], current.Depth + 1, current);
                current.Children.Add(newNode);
                current = newNode;
            }

            current.Children.Add(new Node('$', current.Depth + 1, current));
        }

        public void Delete(string s)
        {
            if (Search(s))
            {
                Node node = Prefix(s).FindChildNode('$');
                
                while(node.IsLeaf)
                {
                    Node parent = node.Parent;
                    parent.DeleteChildNode(node.Value);
                    node = parent;
                }
            }
        }

        public List<string> GetAllStrings(string s, List<string> result)
        {
            var currentNode = Prefix(s);
            if (currentNode.Value == '^') return new List<string>();

            foreach (Node child in currentNode.Children)
            {
                if (child.IsLeaf)
                {
                    result.Add(s);
                    continue;
                }
                
                string newString = new string(s.Append(child.Value).ToArray());
                result = GetAllStrings(newString, result);
            }

            return result;
        }
    }
}
