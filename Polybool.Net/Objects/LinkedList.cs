using System;

namespace Polybool.Net.Objects
{
    public class LinkedList
    {
        public LinkedList()
        {
            root = new Node { IsRoot = true };
        }

        private Node root;

        public Node Root
        {
            get
            {
                return root;
            }
        }

        public bool Exists(Node node)
        {
            if (node == null || node == Root)
                return false;
            return true;
        }

        public bool IsEmpty()
        {
            return Root.Next == null;
        }

        public Node GetHead()
        {
            return Root.Next;
        }

        public void InsertBefore(Node node, Func<Node, bool> check)
        {
            var last = Root;
            var here = Root.Next;
            while (here != null)
            {
                if (check(here))
                {
                    node.Previous = here.Previous;
                    node.Next = here;
                    here.Previous.Next = node;
                    here.Previous = node;
                    return;
                }
                last = here;
                here = here.Next;
            }
            last.Next = node;
            node.Previous = last;
            node.Next = null;
        }

        public Transition FindTransition(Func<Node, bool> check)
        {
            var prev = Root;
            var here = Root.Next;
            while (here != null)
            {
                if (check(here))
                    break;
                prev = here;
                here = here.Next;
            }
            return new Transition
            {
                Before = prev == Root ? null : prev,
                After = here,
                Insert = node =>
                {
                    node.Previous = prev;
                    node.Next = here;
                    prev.Next = node;
                    if (here != null)
                        here.Previous = node;
                    return node;
                }
            };
        }

        public static Node Node(Node data)
        {
            data.Previous = null;
            data.Next = null;
            data.Remove = () =>
            {
                data.Previous.Next = data.Next;
                if (data.Next != null)
                    data.Next.Previous = data.Previous;
                data.Previous = null;
                data.Next = null;
            };
            return data;
        }
    }
}