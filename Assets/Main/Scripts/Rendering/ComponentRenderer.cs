using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class ComponentRenderer : ScriptableObject
{
    [SerializeField]
    protected Material componentMaterial;
    [SerializeField]
    protected string materialBaseProperty;
    [SerializeField]
    protected Color baseColor;

    protected List<GameObject> listeners = new List<GameObject>();

    public abstract void render(GameObject target);

    public abstract void instantiate(GameObject target);

    public void register(GameObject go)
    {
        listeners.Add(go);
    }

    // Add: Unregistering GameObjects

    void OnValidate()
    {
        if (EditorApplication.isPlaying)
        {
            foreach (var c in listeners)
            {
                render(c);
            }
        }
    }

    void OnDisable()
    {
        listeners.Clear();
    }
}