using GraphInterface;
using GraphInterface.Meta;

public class EnvironmentNodeBehaviour : ComponentBehaviour
{
    public int height;

    protected override void Awake()
    {
        base.Awake();

        baseType = BaseType.Environment;

        setRendererFactory(scriptableFactories.nodeRendererFactory);
    }

    public override void analyzeMetadata(Metadata metadata)
    {
        // Extend...
        // Other actions defining behaviour basing on tags in Metadata object
    }
}