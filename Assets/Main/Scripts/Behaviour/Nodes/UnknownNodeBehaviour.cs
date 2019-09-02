using GraphInterface;
using GraphInterface.Meta;

public class UnknownNodeBehaviour : ComponentBehaviour
{    
    protected override void Awake()
    {
        base.Awake();

        baseType = BaseType.Unknown;

        setRendererFactory(scriptableFactories.nodeRendererFactory);
    }

    public override void analyzeMetadata(Metadata metadata)
    {
        // Extend...
        // Other actions defining behaviour basing on tags in Metadata object
    }
}