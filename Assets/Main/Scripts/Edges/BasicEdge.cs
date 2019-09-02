namespace GraphInterface.Edges
{
    sealed class BasicEdge : EdgeComponent
    {
        public override GraphData getGraphData()
        {
            return ExampleGraphData.getInstance();
        }
    }
}
