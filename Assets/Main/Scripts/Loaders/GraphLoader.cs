using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;
using GraphInterface.Meta;
using GraphInterface.Nodes;

namespace GraphInterface.Loaders
{
    public abstract class NodeDataLoader
    {
        protected GraphData graphData;

        public string resourcePath { get; private set; }

        protected NodeDataLoader(string resourcePath)
        {
            this.resourcePath = resourcePath;

            if(!DoesMapResourceExist())
            {
                throw new Exception("Please pride the resource in .txt format.");
            }
        }

        public abstract void LoadNodeData();

        public void saveNodeToGraphData(NodeComponent savedNode)
        {

        }

        private bool DoesMapResourceExist()
        {
            var txtAsset = Resources.Load<TextAsset>($"OSMmaps/{resourcePath}");

            if (txtAsset == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    sealed class HDmapLoader : NodeDataLoader
    {
        public HDmapLoader(string resourcePath) : base(resourcePath)
        {
        }

        public override void LoadNodeData()
        {
            throw new NotImplementedException();
        }
    }
}