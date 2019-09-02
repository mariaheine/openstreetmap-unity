
using GraphInterface;
using UnityEngine;
using GraphInterface.Meta;
using System.Collections.Generic;

namespace GraphInterface.Factories
{
    [CreateAssetMenu(menuName = "RenderBehaviour/NodeRendererFactory", fileName = "NodeRendererFactory")]
    public class NodeRendererFactory : RendererFactory
    {
        [Header("Nodes:", order = 1)]
        [Header("Road Renderers", order = 2)]
        [SerializeField]
        private ComponentRenderer roadNodeRenderer = default;
        [Header("Environment Renderers")]
        [SerializeField]        
        private ComponentRenderer buildingNodeRenderer = default;
        [Header("Default Renderer")]
        [SerializeField]
        private ComponentRenderer unknownNodeRednerer = default;

        public override ComponentRenderer getRenderer(BaseType baseType)
        {
            switch (baseType)
            {
                case BaseType.Road:
                    return roadNodeRenderer;
                case BaseType.Environment:
                    return buildingNodeRenderer;
                default:
                    return unknownNodeRednerer;
            }
        }
    }
}