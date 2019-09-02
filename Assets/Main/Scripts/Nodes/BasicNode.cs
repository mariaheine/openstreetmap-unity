namespace GraphInterface.Nodes
{
    sealed class BasicNode : NodeComponent
    {
        public override GraphData getGraphData()
        {
            return ExampleGraphData.getInstance();
        }
    }
}