using System.Collections;
using System.Collections.Generic;

namespace MvcApplication1.DataBaseStructures
{

    public class IndexedList<T> : IEnumerable<T>
    {
        internal class DNode<TV>
        {
            public DNode<TV> Next;
            public DNode<TV> Prev;
            public TV Value;
        }

        public int Count { private set; get; }
        private DNode<T> Head;

        public IndexedList()
        {
            Head = new DNode<T>();
            Head.Next = Head;
            Head.Prev = Head;
        }

        public void AddToHead(T entry)
        {
            DNode<T> newNode = new DNode<T>();
            newNode.Value = entry;

            newNode.Prev = Head;
            newNode.Next = Head.Next;
            Head.Next = newNode;
            Head.Next.Next.Prev = newNode;

            Count++;
        }

        public void AddToTail(T entry)
        {
            DNode<T> newNode = new DNode<T>();
            newNode.Value = entry;

            newNode.Next = Head;
            newNode.Prev = Head.Prev;
            Head.Prev = newNode;
            Head.Prev.Prev.Next = newNode;

            Count++;
        }

        /*
         * if x > count, it adds to the end.
         */
        public void AddToPosition(T entry, int x) // {x > 0 && x < size}
        {
            DNode<T> curr = GetDNode(x-1);
            DNode<T> newNode = new DNode<T> {Value = entry};

            newNode.Next = curr.Next;
            newNode.Prev = curr.Prev;

            curr.Next.Prev = newNode;
            curr.Next = newNode;

            Count++;
        }

        public void Swap(int x, int y) // {swaps x and y}
        {
            if(x != y)
            {
                DNode<T> nodeX = GetDNode(x);
                DNode<T> nodeY = GetDNode(y);

                T valueX = nodeX.Value;
                nodeX.Value = nodeY.Value;
                nodeY.Value = valueX;
            }
        }

        public T Get(int x) // {x > 0 && x < size}
        {
            return GetDNode(x).Value;
        }

        public void Remove(T value)
        {
            Remove(GetIndexFor(value));
        }

        public void Remove(int x) // {x > 0 && x < size}
        {
            DNode<T> toRemoveNode = GetDNode(x);
            toRemoveNode.Prev.Next = toRemoveNode.Next;
            toRemoveNode.Next.Prev = toRemoveNode.Prev;

            Count--;
        }

        //DNODE AUXILIARY METHODS

        private IEnumerable<DNode<T>> GetDNodeEnumerator()
        {
            DNode<T> currNode = Head.Next;
            while (currNode != Head)
            {
                yield return currNode;
                currNode = currNode.Next;
            }
        }

        private DNode<T> GetDNode(int x)
        {
            DNode<T> curr = Head.Next;

            for (int i = 0; i < x && i < Count; i++)
            {
                curr = curr.Next;
            }

            return curr;
        }

        public int GetIndexFor(T t)
        {
            DNode<T> curr = Head.Next;

            for (int i = 0; i < Count; i++)
            {
                if(t.Equals(curr.Value))
                {
                    return i;
                }
                curr = curr.Next;
            }
            return -1;
        }


        // IENUMERABLE METHODS

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var curr in GetDNodeEnumerator())
            {
                yield return curr.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach( var curr in GetDNodeEnumerator())
            {
                yield return curr.Value;
            }
        }
    }
}
