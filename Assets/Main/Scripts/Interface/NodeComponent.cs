using UnityEngine;
using System.Collections.Generic;
using GraphInterface.Factories;
using GraphInterface.Edges;

namespace GraphInterface.Nodes
{
    public abstract class NodeComponent : GraphComponent
    {
        public List<NodeComponent> InNodes;
        public List<NodeComponent> OutNodes;

        public List<EdgeComponent> InEdges = new List<EdgeComponent>();
        public List<EdgeComponent> OutEdges = new List<EdgeComponent>();

        protected override void Awake()
        {
            base.Awake();
            behaviourFactory = new NodeBehaviourFactory();
        }

        #region GETTERS/SETTERS 

        public void setNodePosition(Vector3 position)
        {
            gameObject.transform.position = position;
        }

        public Vector3 getNodePosition()
        {
            return gameObject.transform.position;
        }

        public void attachEdge(EdgeComponent inEdge = null, EdgeComponent outEdge = null)
        {
            if (inEdge)
                InEdges.Add(inEdge);
            if (outEdge)
                OutEdges.Add(outEdge);
        }

        public void connectNodes(NodeComponent inNode = null, NodeComponent outNode = null)
        {
            if (inNode != null)
                InNodes.Add(inNode);
            if (outNode != null)
                OutNodes.Add(outNode);
        }

        #endregion
    }
}