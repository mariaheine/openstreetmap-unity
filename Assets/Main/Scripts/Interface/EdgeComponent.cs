using GraphInterface.Factories;
using UnityEngine;

namespace GraphInterface.Edges
{
    public abstract class EdgeComponent : GraphComponent
    {
        private EdgePoints edgePoints;

        public struct EdgePoints
        {
            public Vector3[] points
            {
                get; private set;
            }

            public EdgePoints(Vector3 inPoint, Vector3 outPoint)
            {
                this.points = new Vector3[2] { inPoint, outPoint };
            }
        }

        protected override void Awake()
        {
            base.Awake();
            behaviourFactory = new EdgeBahaviourFactory();
        }

        #region GETTERS/SETTERS

        public void setEdgePoints(Vector3 inPoint, Vector3 outPoint)
        {
            edgePoints = new EdgePoints(inPoint, outPoint);
        }

        public EdgePoints getEdgePoints()
        {
            return edgePoints;
        }

        #endregion
    }
}