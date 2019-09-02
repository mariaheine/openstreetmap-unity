using UnityEngine;
using System.Xml;
using GraphInterface.Factories;

namespace GraphInterface.Nodes
{
    interface NodeBuilder<T>
    {
        void reset();
        void passNodeData(T nodeData);
        void buildNodeID();
        void buildNodePositionWithOffset(Vector3 offset);
        void processMetadata(ulong metadataID);
        NodeComponent getNode();
    }

    sealed class OsmNodeBuilder : NodeBuilder<XmlNode>
    {
        private NodeComponent node;
        private XmlNode nodeData;

        public void reset()
        {
            node = GraphComponentFactory.createNode(NodeType.BasicNode);
        }

        public void passNodeData(XmlNode nodeData)
        {
            this.nodeData = nodeData;
        }

        public void buildNodeID()
        {
            ulong ID = XmlAttributeParser
                .GetAttribute<ulong>("id", nodeData.Attributes);
                
            node.setID(ID);
            node.setGameObjectName($"Node_{node.getID()}");
        }

        public void buildNodePositionWithOffset(Vector3 offset)
        {


            float Latitude = XmlAttributeParser
                .GetAttribute<float>("lat", nodeData.Attributes);
            float Longitude = XmlAttributeParser
                .GetAttribute<float>("lon", nodeData.Attributes);

            // Debug.Log(Latitude + " " + Longitude);

            float X = (float)MercatorProjection
                .lonToX(Longitude) - offset.x;
            float Z = (float)MercatorProjection
                .latToY(Latitude) - offset.z;
            float Y = 0;

            node.setNodePosition(new Vector3(X, Y, Z));

            // Debug.Log(new Vector3(X, Y, Z));
        }

        public void processMetadata(ulong metadataID)
        {
            node.assignMetadataID(metadataID);
            node.analyzeMetadata();
        }

        public NodeComponent getNode()
        {
            return node;
        }
    }
}