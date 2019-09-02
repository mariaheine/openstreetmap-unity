using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;
using GraphInterface.Meta;
using GraphInterface.Nodes;

namespace GraphInterface.Loaders
{
    sealed class OsmLoader : NodeDataLoader
    {
        private NodeBuilder<XmlNode> nodeBuilder;
        private Dictionary<ulong, XmlNode> preloadNodes;
        private Vector3 BoundsCentre;

        public OsmLoader(string resourcePath) : base(resourcePath)
        {
            preloadNodes = new Dictionary<ulong, XmlNode>();
            graphData = ExampleGraphData.getInstance();
            nodeBuilder = new OsmNodeBuilder();
        }


        public override void LoadNodeData()
        {
            var txtAsset = Resources.Load<TextAsset>($"OSMmaps/{resourcePath}");

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(txtAsset.text);

            GetOsmBoundsCentre(doc.SelectSingleNode("/osm/bounds"));
            LoadOsmNodes(doc.SelectNodes("/osm/node"));
            LoadOsmWays(doc.SelectNodes("/osm/way"));
        }

        private void GetOsmBoundsCentre(XmlNode node)
        {
            var MaxLat = XmlAttributeParser
                .GetAttribute<float>("maxlat", node.Attributes);
            var MinLat = XmlAttributeParser
                .GetAttribute<float>("minlat", node.Attributes);
            var MinLon = XmlAttributeParser
                .GetAttribute<float>("minlon", node.Attributes);
            var MaxLon = XmlAttributeParser
                .GetAttribute<float>("maxlon", node.Attributes);

            // Create the centre location of OSM data
            float x = (float)((MercatorProjection
                .lonToX(MaxLon) + MercatorProjection.lonToX(MinLon)) / 2);
            float z = (float)((MercatorProjection
                .latToY(MaxLat) + MercatorProjection.latToY(MinLat)) / 2);

            BoundsCentre = new Vector3(x, 0, z);
        }

        private void LoadOsmNodes(XmlNodeList xmlNodeList)
        {
            foreach (XmlNode n in xmlNodeList)
            {
                ulong ID = XmlAttributeParser.GetAttribute<ulong>("id", n.Attributes);
                preloadNodes[ID] = n;
            }
        }

        private void LoadOsmWays(XmlNodeList xmlNodeList)
        {
            foreach (XmlNode way in xmlNodeList)
            {
                LoadOsmWayTags(way);
                BuildNodesFromWay(way);
            }
        }

        private void LoadOsmWayTags(XmlNode way)
        {
            ulong wayID = XmlAttributeParser.GetAttribute<ulong>("id", way.Attributes);
            XmlNodeList wayTags = way.SelectNodes("tag");

            graphData.metadata.AddItem(wayID, new Meta.Metadata());

            var wayMetadata = graphData.metadata.GetItem(wayID);

            foreach (XmlNode t in wayTags)
            {
                string key = XmlAttributeParser.GetAttribute<string>("k", t.Attributes);
                string value = XmlAttributeParser.GetAttribute<string>("v", t.Attributes);

                Metatag tag = new Metatag(key, value);

                wayMetadata.addMetatag(tag);
            }
        }

        private void BuildNodesFromWay(XmlNode way)
        {
            XmlNodeList referencedNodes = way.SelectNodes("nd");

            ulong wayID = XmlAttributeParser.GetAttribute<ulong>("id", way.Attributes);

            Transform nodeAttachParent = GameObject.Find("Nodes").transform;

            if (graphData.metadata.ContainsID(wayID))
            {
                var wayMetadata = graphData.metadata.GetItem(wayID);

                if (wayMetadata.containsMetatagPair(key: "boundary", value: "postal_code") ||
                    wayMetadata.containsMetatagKey("frequency"))
                {
                    // Skip uninteresting osm data
                    return;
                }

                for (int i = 0; i < referencedNodes.Count; i++)
                {
                    ulong nodeID = XmlAttributeParser
                        .GetAttribute<ulong>("ref", referencedNodes[i].Attributes);

                    if (wayMetadata.containsComponentWithID(nodeID))
                    {
                        // In certain cases a single node may appear twice
                        // in a single osm way (forming a loop)

                        NodeComponent existingNode = graphData.nodes.GetItem(nodeID);
                        wayMetadata.addComponent(existingNode);
                    }
                    else if (graphData.nodes.ContainsID(nodeID))
                    {
                        // Often single node may be present in multiple
                        // osm ways, if node with certain ID is already created,
                        // again only add the reference

                        NodeComponent existingNode = graphData.nodes.GetItem(nodeID);
                        wayMetadata.addComponent(existingNode);
                    }
                    else
                    {
                        /* BUILD NEW NODE */
                        XmlNode nodeData = preloadNodes[nodeID];

                        nodeBuilder.reset();
                        nodeBuilder.passNodeData(nodeData);
                        nodeBuilder.buildNodeID();
                        nodeBuilder.buildNodePositionWithOffset(BoundsCentre);
                        nodeBuilder.processMetadata(wayID);

                        NodeComponent newNode = nodeBuilder.getNode();
                        graphData.nodes.AddItem(newNode.getID(), newNode);
                        wayMetadata.addComponent(newNode);

                        newNode.gameObject.transform.parent = nodeAttachParent;
                    }
                }
            }
            else
            {
                throw new NamedException($"Way with {wayID} ID was not instantantiated.");
            }
        }
    }
}