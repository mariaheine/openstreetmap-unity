
using System.Collections.Generic;
using GraphInterface.Factories;
using UnityEngine;

namespace GraphInterface
{
    [CreateAssetMenu(
        menuName = "RenderBehaviour/NodeRenderer",
        fileName = "NodeRenderer")]
    public class NodeRenderer : ComponentRenderer
    {
        [SerializeField]
        private Mesh mesh = default;
        [SerializeField]
        [Range(0.2f, 1f)]
        private float scale = 0.5f;

        public override void instantiate(GameObject target)
        {
            var meshFilter = target.AddComponent<MeshFilter>();
            meshFilter.mesh = mesh;
            var renderer = target.AddComponent<MeshRenderer>();
        }

        public override void render(GameObject target)
        {
            var renderer = target.GetComponent<Renderer>();
            if (renderer == null)
            {
                Debug.LogError("Node is missing Renderer component", target.transform);
                return;
            }

            target.transform.localScale = new Vector3(scale,scale,scale);

            renderer.material = componentMaterial;
            var material = renderer.sharedMaterial;
            material.SetColor(materialBaseProperty, baseColor);
        }

    }
}