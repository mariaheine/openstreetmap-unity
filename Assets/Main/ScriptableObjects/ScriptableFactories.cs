using UnityEngine;
using GraphInterface.Factories;

[RequireComponent(typeof(Transform))]
public class ScriptableFactories : MonoBehaviour
{
    private static ScriptableFactories instance;

    public static ScriptableFactories getInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
    }

    public NodeRendererFactory nodeRendererFactory;
    public EdgeRendererFactory edgeRendererFactory;
}
