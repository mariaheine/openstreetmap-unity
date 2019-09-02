using System;
using GraphInterface.Factories;
using GraphInterface.Meta;
using UnityEngine;

namespace GraphInterface
{    
    public enum environmentKeys
    {
        building,
        landuse,
        barrier
    };

    public abstract class GraphComponent : MonoBehaviour
    {
        [SerializeField] protected ulong ID = 0;
        [SerializeField] protected ulong metadataID = 0;

        protected ComponentBehaviour componentBehaviour;

        public GraphData graphData { get; private set; }
        public BehaviourFactory behaviourFactory { get; protected set; }

        public abstract GraphData getGraphData();

        protected virtual void Awake()
        {
            graphData = getGraphData();
        }

        public void setID(ulong ID)
        {
            this.ID = ID;
        }

        public void setGameObjectName(string name)
        {
            gameObject.name = name;
        }

        public ulong getID()
        {
            return ID;
        }

        public void assignMetadataID(ulong metadataID)
        {
            this.metadataID = metadataID;
        }

        public ulong getMetadataID()
        {
            return metadataID;
        }

        public void analyzeMetadata()
        {
            if (metadataID != 0)
            {
                Metadata metadata = graphData.metadata.GetItem(metadataID);

                createBehaviour(metadata, behaviourFactory);

                componentBehaviour.analyzeMetadata(metadata);

                componentBehaviour.setRenderer();
            }
            else
            {
                Debug.LogError("Node is missing metadata reference", transform);
            }
        }

        public void createBehaviour(Metadata metadata, BehaviourFactory behaviourFactory)
        {
            if (gameObject.GetComponent<ComponentBehaviour>())
            {
                Debug.LogWarning("ComponentBehaviour is already attached.", transform);
            }
            else
            {
                foreach (var key in Enum.GetValues(typeof(environmentKeys)))
                {
                    if (metadata.containsMetatagKey(key: key.ToString()))
                    {
                        componentBehaviour = behaviourFactory
                            .CreateBehaviour(gameObject, BaseType.Environment);
                        return;
                    }
                }

                if (metadata.containsMetatagKey(key: "highway"))
                {
                    componentBehaviour = behaviourFactory
                        .CreateBehaviour(gameObject, BaseType.Road);
                }
                else
                {
                    componentBehaviour = behaviourFactory
                        .CreateBehaviour(gameObject, BaseType.Unknown);
                }
            }
        }
    }
}
