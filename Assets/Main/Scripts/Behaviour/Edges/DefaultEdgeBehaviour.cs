using System;
using GraphInterface;
using GraphInterface.Meta;

public class DefaultEdgeBehaviour : ComponentBehaviour
{
    protected override void Awake()
    {
        base.Awake();

        baseType = BaseType.Road;

        setRendererFactory(scriptableFactories.edgeRendererFactory);
    }

    public override void analyzeMetadata(Metadata metadata)
    {
        findBaseType(metadata);

        // Extend...
        // Other actions defining behaviour basing on tags in Metadata object
    }

    private void findBaseType(Metadata metadata)
    {
        foreach (var key in Enum.GetValues(typeof(environmentKeys)))
        {
            if (metadata.containsMetatagKey(key: key.ToString()))
            {
                setBaseType(BaseType.Environment);
                return;
            }
        }

        if (metadata.containsMetatagKey(key: "highway"))
        {
            setBaseType(BaseType.Road);
        }
        else
        {
            setBaseType(BaseType.Unknown);
        }
    }
}