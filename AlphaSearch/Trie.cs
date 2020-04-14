using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaSearch
{
    public class Trie<T> where T : class
    {
        public bool AllowStoreDuplicates { get; protected set; }
        public bool AllowReturnDuplicates { get; protected set; }

        private readonly Node<T> _root;

        public Trie(bool allowStoreDuplicates, bool allowReturnDuplicates)
        {
            _root = Node<T>.CreateRootNode();
            AllowStoreDuplicates = allowStoreDuplicates;
            AllowReturnDuplicates = allowReturnDuplicates;
        }

        private Node<T> Prefix(string s)
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
            Node<T> prefix = Prefix(s);
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
            Node<T> commonPrefix = Prefix(s);
            if (AllowStoreDuplicates == false && commonPrefix.Depth == s.Length) return;

            Node<T> current = commonPrefix;

            for (int i = current.Depth; i < s.Length; i++)
            {
                Node<T> newNode = new Node<T>(s[i], null, current.Depth + 1, current);
                current.Children.Add(newNode);
                current = newNode;
            }

            current.Children.Add(new Node<T>('$', null, current.Depth + 1, current));
        }

        public void Delete(string s)
        {
            if (Search(s))
            {
                Node<T> node = Prefix(s).FindChildNode('$');
                
                while(node.IsLeaf)
                {
                    Node<T> parent = node.Parent;
                    parent.DeleteChildNode(node.Value);
                    node = parent;
                }
            }
        }

        public List<string> GetAllStrings(string s, List<string> result)
        {
            var currentNode = Prefix(s);
            if (currentNode.Value == '^') return new List<string>();

            foreach (Node<T> child in currentNode.Children)
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
