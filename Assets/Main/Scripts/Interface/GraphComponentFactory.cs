using UnityEngine;
using GraphInterface.Nodes;
using GraphInterface.Edges;

namespace GraphInterface.Factories
{
    public enum NodeType { BasicNode }
    public enum EdgeType { BasicEdge }

    public class GraphComponentFactory
    {
        public static NodeComponent createNode(NodeType type)
        {
            var nodeBase = new GameObject();

            switch (type)
            {
                case NodeType.BasicNode:                        
                    return nodeBase.AddComponent<BasicNode>();
                default:
                    throw new NotImplementedTypeException($"{type} type is not implemented.");
            }
        }

        public static EdgeComponent createEdge(EdgeType type)
        {
            var edgeBase = new GameObject();

            switch (type)
            {
                case EdgeType.BasicEdge:                    
                    return edgeBase.AddComponent<BasicEdge>();
                default:
                    throw new NotImplementedTypeException($"{type} type is not implemented.");
            }
        }
    }

    public class NotImplementedTypeException : System.Exception
    {
        public NotImplementedTypeException(string message) : base(message) { }
    }
}
