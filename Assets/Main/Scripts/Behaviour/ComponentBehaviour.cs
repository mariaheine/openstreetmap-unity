using GraphInterface.Meta;
using UnityEngine;

public enum BaseType { Unknown, Road, Environment }

namespace GraphInterface
{
    public abstract class ComponentBehaviour : MonoBehaviour
    {
        [SerializeField]
        protected ComponentRenderer nodeRenderer;
        [SerializeField]
        protected BaseType baseType;

        public ScriptableFactories scriptableFactories { get; protected set; }
        public RendererFactory rendererFactory { get; protected set; }

        public abstract void analyzeMetadata(Metadata metadata);

        protected virtual void Awake()
        {
            scriptableFactories = ScriptableFactories.getInstance();
        }

        protected void setRendererFactory(RendererFactory rendererFactory)
        {
            this.rendererFactory = rendererFactory;
        }        

        protected void setBaseType(BaseType baseType)
        {
            this.baseType = baseType;
        }

        public virtual void setRenderer()
        {
            nodeRenderer = rendererFactory.getRenderer(baseType);
            nodeRenderer.instantiate(gameObject);
            nodeRenderer.register(gameObject);
            nodeRenderer.render(gameObject);
        }
    }
}