using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphInterface.Meta
{
    public struct Metatag
    {
        public string key;
        public string value;

        public Metatag(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
    }

    public class Metadata
    {
        private List<GraphComponent> attachedComponents = new List<GraphComponent>();
        private List<Metatag> metatags = new List<Metatag>();

        public List<GraphComponent> getComponents()
        {
            return attachedComponents;
        }

        public void addComponent(GraphComponent component)
        {
            attachedComponents.Add(component);
        }

        public GraphComponent getComponent(ulong ID)
        {
            return attachedComponents.Find(c => c.getID() == ID);
        }

        public void removeComponent(GraphComponent component)
        {
            attachedComponents.Remove(component);
        }

        public bool containsComponentWithID(ulong ID)
        {
            foreach (GraphComponent c in attachedComponents)
            {
                if (ID.Equals(c.getID()))
                    return true;
            }
            return false;
        }

        public void addMetatag(Metatag tag)
        {
            metatags.Add(tag);
        }

        public string getMetatagValue(string key)
        {
            foreach (Metatag tag in metatags)
            {
                if (tag.key == key)
                {
                    return tag.value;
                }
            }
            return null;
        }

        public bool containsMetatagKey(string key)
        {
            foreach (Metatag tag in metatags)
            {
                if (tag.key == key)
                    return true;
            }
            return false;
        }

        public bool containsMetatagPair(string key, string value)
        {
            foreach (Metatag tag in metatags)
            {
                if (tag.key == key && tag.value == value)
                    return true;
            }
            return false;
        }

        /* Intermal iterator, unused */
        // public delegate void metatagInterpreter(Metatag tag);

        // public void analyzeMetatags(metatagInterpreter interpreter)
        // {
        //     foreach (Metatag tag in metatags)
        //     {
        //         interpreter(tag);
        //     }
        // }

        // Add: Removing components and metatags
    }
}
