using System.Collections.Generic;
using GraphInterface;
using GraphInterface.Edges;
using GraphInterface.Meta;
using GraphInterface.Nodes;

public class WayMetadata : Data<Metadata>
{
    public WayMetadata()
    {
        dataSet = new Dictionary<ulong, Metadata>();
    }
}
public class NodeData : Data<NodeComponent>
{
    public NodeData()
    {
        dataSet = new Dictionary<ulong, NodeComponent>();
    }
}

public class EdgeData : Data<EdgeComponent>
{
    public EdgeData()
    {
        dataSet = new Dictionary<ulong, EdgeComponent>();
    }
}

public class ExampleGraphData : GraphData
{
    private static ExampleGraphData instance;

    public ExampleGraphData()
    {
        metadata = new WayMetadata();
        nodes = new NodeData();
        edges = new EdgeData();
    }

    public static ExampleGraphData getInstance()
    {
        if (instance == null)
        {
            instance = new ExampleGraphData();
        }
        return instance;
    }
}