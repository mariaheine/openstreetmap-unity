using UnityEngine;

public abstract class RendererFactory : ScriptableObject
{
    public abstract ComponentRenderer getRenderer(BaseType BaseType);
}