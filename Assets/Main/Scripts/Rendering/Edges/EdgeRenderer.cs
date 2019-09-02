using GraphInterface.Edges;
using UnityEngine;

namespace GraphInterface
{
    [CreateAssetMenu(
        menuName = "RenderBehaviour/EdgeRenderer",
        fileName = "EdgeRenderer")]
    public class EdgeRenderer : ComponentRenderer
    {
        [Range(0.2f, 1.5f)]
        public float lineWidth = 0.5f;

        public override void instantiate(GameObject target)
        {
            var filter = target.AddComponent<LineRenderer>();
        }

        public override void render(GameObject target)
        {
            var line = target.GetComponent<LineRenderer>();
            var edge = target.GetComponent<EdgeComponent>();

            if (line == null)
            {
                Debug.LogError("Edge is missing LineRenderer component", target.transform);
                return;
            }
            else if (edge == null)
            {
                Debug.LogError("Target GameObject is missing EdgeComponent script", target.transform);
                return;
            }

            line.SetPositions(edge.getEdgePoints().points);
            SetLineWidth(line);
            line.sharedMaterial = componentMaterial;
            line.sharedMaterial.SetColor(materialBaseProperty, baseColor);
        }

        private void SetLineWidth(LineRenderer line)
        {
            AnimationCurve curve = new AnimationCurve();
            curve.AddKey(0.0f, 1.0f);
            curve.AddKey(1.0f, 1.0f);
            line.widthCurve = curve;
            line.widthMultiplier = lineWidth;
        }
    }
}