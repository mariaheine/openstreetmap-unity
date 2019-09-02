using UnityEngine;
using GraphInterface;
using GraphInterface.Edges;

public class Initializer : MonoBehaviour
{
    private enum ExampleOSMMaps { Park, Another, Huge, Custom } 

    private GraphData graphData;
    [SerializeField] private ExampleOSMMaps SelectedMap = ExampleOSMMaps.Park;
    [SerializeField] private string CustomMap;

    void Start()
    {
        graphData = ExampleGraphData.getInstance();

        string selectedMapName;

        if(SelectedMap == ExampleOSMMaps.Custom)
        {
            selectedMapName = CustomMap;
        }
        else
        {
            selectedMapName = SelectedMap.ToString();
        }

        var loader = DataLoaderFactory.createLoader(LoaderType.OsmLoader, selectedMapName);
        loader.LoadNodeData();

        EdgeBuilder edgeBuilder = new DefaultEdgeBuilder();
        edgeBuilder.getNodePairs();
        edgeBuilder.spawnEdges();
    }
}

