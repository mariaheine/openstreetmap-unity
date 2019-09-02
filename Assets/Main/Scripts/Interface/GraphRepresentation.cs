using System.Collections;
using System.Collections.Generic;
using GraphInterface.Edges;
using GraphInterface.Meta;
using GraphInterface.Nodes;

namespace GraphInterface
{
    public abstract class GraphData
    {
        public Data<Metadata> metadata { get; protected set; }
        public Data<NodeComponent> nodes { get; protected set; }
        public Data<EdgeComponent> edges { get; protected set; }
    }
    
    public abstract class Data<T>
    {
        protected Dictionary<ulong, T> dataSet;

        public int Count
        {
            get { return dataSet.Count; }
        }

        public void AddItem(ulong id, T item)
        {
            dataSet.Add(id, item);
        }

        public void SetItem(ulong id, T item)
        {
            dataSet[id] = item;
        }

        public T GetItem(ulong ID)
        {
            return dataSet[ID];
        }

        public IEnumerator getEnumerator()
        {
            return new DictionaryEnumerator<T>(dataSet.Values);
        }

        public bool ContainsID(ulong ID)
        {
            return dataSet.ContainsKey(ID);
        }
    }

    public abstract class BasicEnumerator<T> : IEnumerator
    {
        protected T[] data;
        protected int position = 0;

        public object Current
        {
            get
            {
                return data[position];
            }
        }

        public bool MoveNext()
        {
            position++;

            if (position >= data.Length)
                return false;
            else
                return true;
        }

        public void Reset()
        {
            position = 0;
        }
    }

    public class DictionaryEnumerator<T> : BasicEnumerator<T>
    {
        public DictionaryEnumerator(Dictionary<ulong, T>.ValueCollection values)
        {
            data = new T[values.Count];
            values.CopyTo(data, 0);
        }
    }

    public class ListEnumerator<T> : BasicEnumerator<T>
    {
        public ListEnumerator(List<T> values)
        {
            data = new T[values.Count];
            values.CopyTo(data, 0);
        }
    }

    public class NamedException : System.Exception
    {
        public NamedException(string message) : base(message) { }
    }
}
