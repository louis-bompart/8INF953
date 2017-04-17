using System;

[Serializable]
public class Node
{
    public MapSaveStateSerializable saveStateSerializable;
    [NonSerialized]
    public MapSaveState saveState;
    //null if is root
    public Node parent;
    public bool isRoot;

    public Node(MapSaveState saveState, Node parent)
    {
        this.parent = parent;
        this.saveState = saveState;
        this.saveStateSerializable = new MapSaveStateSerializable(saveState);
        this.isRoot = false;
    }
    public static Node CreateRoot(MapSaveState saveState)
    {
        Node toReturn = new Node(saveState, null);
        toReturn.isRoot = true;
        return toReturn;
    }
}