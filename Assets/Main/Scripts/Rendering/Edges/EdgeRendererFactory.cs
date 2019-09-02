
using GraphInterface;
using UnityEngine;
using GraphInterface.Meta;
using System.Collections.Generic;

namespace GraphInterface.Factories
{
    [CreateAssetMenu(menuName = "RenderBehaviour/EdgeRendererFactory", fileName = "EdgeRendererFactory")]
    public class EdgeRendererFactory : RendererFactory
    {
        [Header("Edges:", order = 1)]
        [Header("Road Renderers", order = 2)]
        [SerializeField]
        private ComponentRenderer roadEdgeRenderer = default;
        [Header("Environment Renderers")]
        [SerializeField]        
        private ComponentRenderer buildingEdgeRenderer = default;
        [Header("Default Renderer")]
        [SerializeField]
        private ComponentRenderer unknownEdgeRednerer = default;

        public override ComponentRenderer getRenderer(BaseType baseType)
        {
            switch (baseType)
            {
                case BaseType.Road:
                    return roadEdgeRenderer;
                case BaseType.Environment:
                    return buildingEdgeRenderer;
                default:
                    return unknownEdgeRednerer;
            }
        }
    }
}