using System;
using GraphInterface;
using GraphInterface.Meta;

public class RoadNodeBehaviour : ComponentBehaviour
{
    private enum RoadType {  }

    public string streetName;
    public int lanes;

    protected override void Awake()
    {
        base.Awake();

        baseType = BaseType.Road;

        setRendererFactory(scriptableFactories.nodeRendererFactory);
    }

    public override void analyzeMetadata(Metadata metadata)
    {
        if(metadata.containsMetatagKey("name"))
        {
            streetName = metadata.getMetatagValue("name");
        }

        if(metadata.containsMetatagKey("lanes"))
        {
            lanes = Convert.ToInt32(metadata.getMetatagValue("lanes"));
        }
    }
}