using UnityEngine;

namespace GraphInterface.Factories
{
    public abstract class BehaviourFactory
    {
        private static BehaviourFactory instance;

        public static BehaviourFactory getInstance()
        {
            if (instance == null)
            {
                instance = new NodeBehaviourFactory();
            }
            return instance;
        }

        public abstract ComponentBehaviour CreateBehaviour(GameObject targetNode, BaseType baseType);
    }

    public class NodeBehaviourFactory : BehaviourFactory
    {
        public override ComponentBehaviour CreateBehaviour(GameObject targetNode, BaseType baseType)
        {
            switch (baseType)
            {
                case BaseType.Road:
                    return targetNode.AddComponent<RoadNodeBehaviour>();
                case BaseType.Environment:
                    return targetNode.AddComponent<EnvironmentNodeBehaviour>();
                default:
                    return targetNode.AddComponent<UnknownNodeBehaviour>();
            }
        }
    }

    public class EdgeBahaviourFactory : BehaviourFactory
    {
        public override ComponentBehaviour CreateBehaviour(GameObject targetNode, BaseType baseType)
        {
            switch (baseType)
            {
                case BaseType.Road:
                case BaseType.Environment:
                default:
                    return targetNode.AddComponent<DefaultEdgeBehaviour>();
            }
        }
    }
}