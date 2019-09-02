# Street Graph Assignment

Documentation for a past job test assignment by Maria Heine. The task was more or less to propose an API for displaying street node and edge data inside Unity3D engine. 

Project includes three example osm maps. The one loaded on editor play can be changed in Unity inspector through `Runtime` GameObject’s `Initializer` MonoBehaviour.

To test the project with any custom map exported from [OpenStreetMap](https://www.openstreetmap.org/export) please change the file format from `.osm` to `.txt`, put the file in `Assets/Resources/OSMmaps`, set `Initializer's` dropdown to `Custom` and provide a file name in `Custom Map` string field.
⚠️ Please restrain the exported map size to about 3x3 kilometers, the
program doesn't implement map chunking and you could freeze Unity3D`.

### 🌌 Approach

-   Considered required elements of the API
    
    -   Importing graph data
        
    -   Runtime storing/management of graph data
        
    -   Rendering of graph data
        
-   Looked for an existing solution for importing of OSM data
    
    -   Found a public repository from which I borrowed parts of code responsible for parsing Xml data (including `MercatorProjection`)
-   Considered Decorator and Strategy patterns for the `GraphComponent`'s implementations of type-specific functionalities (eg. different rendering methods of street node or building node). I decided upon Strategy to avoid possibly large quantities of subclasses and to allow easy runtime changing of component behaviour. This is reflected in both `ComponentBehaviour` and `ComponentRenderer`.
    

### 🌌 Notes

#### ⭐️ Graph data loading

For the sake off this assignment I have implemented a osm (OpenStreetMap) data loader, borrowing parts of the code responsible for parsing Xml data from a public repository (to be found [here](https://github.com/codehoose/real-world-map-data)). The `OsmLoader` is an implementation of the abstract class `NodeDataLoader`, the API could be extended by other implementations interpreting HD map data for example.

#### ⭐️ Runtime graph representation

The street graph data is represented in `GraphData` by a set of three Dictionary collections storing metadata as well as node and edge data. Dictionaries allow for quick key-based queries, which make them a good candidate for graph data containers.

Those dictionaries aren’t publicly exposed in order to allow control over how data is manipulated (`Data<T>` contains definitions of possible interactions).

#### ⭐️ Storing and associating metadata

Nodes and edges share a base abstract class `GraphComponent` which defines common operations and behaviour shared by those elements. This includes association of metadata which they reference by it’s ID in `GraphData` collection.

`Metadata` objects store lists of key-value string pairs (`Metatag` struct) and control methods of accessing them. I implemented this behaviour as a mirror to how data is being represented in osm data (standardized key-value string pairs).

#### ⭐️ Rendering API

Rendering of both edges (`EdgeComponent`) and nodes (`NodeComponent`) is controlled by implementations of an abstract _ScriptableObject_ `ComponentRenderer`.

-   Sharing a common interface allows for an extension of the rendering API without introducing changes in the existing code.
    
-   Using ScriptableObject instead of MonoBehaviour as a base class of rendering interface allows for easy customization and runtime testing of rendering methods (changes in SO assets are reflected in realtime and serialized when leaving the play mode).
    

### 🌌 Solution benefits, scalability, possible pitfalls

-   Abstraction of `GraphComponent` allows for extension of it’s functionality without altering existing implementations. It also follows the idea of interface-based API programming.
    
-   Repeated use of Factory pattern in the assignment helps to separate responsibilities of classes and monitor object instantiation behaviour.
    
-   Pitfall: edges copy their metadata from starting node, quite likely there will be a need for a completely separate type of metadata appropriate solely for edge-type components.
    
-   Pitfall: all data loading in `OsmLoader` currently takes place on a single thread (responsible for loading time for large osm maps)
