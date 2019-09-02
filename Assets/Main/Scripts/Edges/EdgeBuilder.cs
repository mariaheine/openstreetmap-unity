using System.Collections.Generic;
using GraphInterface.Nodes;
using GraphInterface.Meta;
using GraphInterface.Factories;
using UnityEngine;

namespace GraphInterface.Edges
{
    public abstract class EdgeBuilder
    {
        protected List<NodePair> nodePairs;
        protected GraphData graphData;

        protected struct NodePair
        {
            public NodeComponent inNode;
            public NodeComponent outNode;

            public NodePair(NodeComponent inNode, NodeComponent outNode)
            {
                this.inNode = inNode;
                this.outNode = outNode;
            }

            private void interconnectNodePair()
            {
                inNode.connectNodes(outNode: outNode);
                outNode.connectNodes(inNode: inNode);
            }
        }

        public EdgeBuilder()
        {
            nodePairs = new List<NodePair>();
        }

        public abstract void getNodePairs();

        public abstract void spawnEdges();

        protected void addNodePair(NodeComponent inNode, NodeComponent outNode)
        {
            NodePair newPair = new NodePair(inNode, outNode);
            nodePairs.Add(newPair);
        }

        protected void attachEdgeToNodes(NodePair nodePair, EdgeComponent attachedEdge)
        {
            nodePair.inNode.attachEdge(outEdge: attachedEdge);
            nodePair.outNode.attachEdge(inEdge: attachedEdge);
        }

        protected void saveEdgeToGraphData(EdgeComponent savedEdge)
        {
            graphData.edges.AddItem(savedEdge.getID(), savedEdge);
        }
    }


    public class DefaultEdgeBuilder : EdgeBuilder
    {
        public DefaultEdgeBuilder() : base()
        {
            graphData = ExampleGraphData.getInstance();
        }

        public override void getNodePairs()
        {
            var wayMetadataEnum = graphData.metadata.getEnumerator();

            do
            {
                Metadata wayData = (Metadata)wayMetadataEnum.Current;

                List<GraphComponent> tempData = wayData.getComponents();

                int componentsCount = wayData.getComponents().Count;

                for (int i = 0; i < componentsCount - 1; i++)
                {
                    if (componentsCount == 1)
                    {
                        break;
                    }

                    NodeComponent inNode = (NodeComponent)tempData[i];
                    NodeComponent outNode = (NodeComponent)tempData[i + 1];

                    addNodePair(inNode, outNode);
                }

            } while (wayMetadataEnum.MoveNext());
        }

        public override void spawnEdges()
        {
            Transform edgeAttachParent = GameObject.Find("Edges").transform;

            foreach (var nodePair in nodePairs)
            {
                EdgeComponent newEdge = GraphComponentFactory.createEdge(EdgeType.BasicEdge);

                /* 
                    Generate edgeID just by counting existing edges; 
                    ulong to keep consistency between other managed collections in GraphData
                */
                ulong edgeID = System.Convert.ToUInt32(graphData.edges.Count + 1);
                newEdge.setID(edgeID);

                newEdge.setGameObjectName($"Edge_{newEdge.getID()}");

                Vector3 inPosition = nodePair.inNode.getNodePosition();
                Vector3 outPosition = nodePair.outNode.getNodePosition();
                newEdge.setEdgePoints(inPosition, outPosition);

                // Copy metadataID from the edge starting node
                ulong copiedMetadataID = nodePair.inNode.getMetadataID();
                newEdge.assignMetadataID(copiedMetadataID);

                newEdge.analyzeMetadata();

                attachEdgeToNodes(nodePair, newEdge);

                saveEdgeToGraphData(newEdge);

                newEdge.transform.parent = edgeAttachParent;
            }
        }
    }
}