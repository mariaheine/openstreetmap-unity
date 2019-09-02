using GraphInterface.Loaders;

public enum LoaderType { OsmLoader, HDMapLoader }

    public class DataLoaderFactory
    {
        public static NodeDataLoader createLoader(LoaderType type, string resourcePath)
        {
            switch (type)
            {
                case LoaderType.OsmLoader:
                    return new OsmLoader(resourcePath);
                case LoaderType.HDMapLoader:
                    return new HDmapLoader(resourcePath);
                default:
                    return null;
            }
        }
    }
