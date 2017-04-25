using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NodeData
{
    public MapSaveStateSerializable saveStateSerializable;
    [NonSerialized]
    public MapSaveState saveState;
    //null if is root
    public NodeData parent;
    public NodeData mergeOrigin;
    public bool isAMerge;
    [NonSerialized]
    public Node node;
    public int depth;
    public bool isRoot;

    public NodeData()
    {

    }

    public NodeData(MapSaveState saveState, NodeData parent)
    {
        this.parent = parent;
        this.saveState = saveState;
        this.depth = parent.depth + 1;
        this.saveStateSerializable = new MapSaveStateSerializable(saveState);
        this.isRoot = false;
        this.isAMerge = false;
    }

    public static NodeData CreateMergeNode(MapSaveState saveState, NodeData into, NodeData from)
    {
        NodeData toReturn = new NodeData(saveState, into);
        toReturn.depth = Mathf.Max(into.depth, from.depth) + 1;
        toReturn.isAMerge = true;
        toReturn.mergeOrigin = from;
        return toReturn;
    }

    public static NodeData CreateRoot(MapSaveState saveState)
    {
        NodeData toReturn = new NodeData(saveState, null);
        toReturn.isRoot = true;
        toReturn.depth = 0;
        toReturn.isAMerge = false;
        return toReturn;
    }
    public Node GetNode()
    {
        if (node == null)
            node = Node.CreateFromData(this);
        return node;
    }
}

public class Node : MonoBehaviour
{
    public static Connection connectionPrefab;
    public static GameObject original;
    public Node parent;
    public NodeData data;
    public bool isRoot;
    public bool isAMerge;
    public Node mergeOrigin;
    private List<Connection> connections;

    internal static Node CreateFromData(NodeData nodeData)
    {
        GameObject toReturnGO = Instantiate(original);
        Node toReturn = toReturnGO.GetComponent<Node>();

        toReturn.connections = new List<Connection>();
        toReturn.isRoot = nodeData.isRoot;
        toReturn.parent = nodeData.parent.GetNode();
        toReturn.data = nodeData;
        toReturn.isAMerge = nodeData.isAMerge;
        if (nodeData.isAMerge)
        {
            toReturn.isAMerge = nodeData.mergeOrigin.GetNode();
            Connection connectionMerged = Instantiate<Connection>(connectionPrefab);
            connectionMerged.SetTargets(toReturnGO.GetComponent<RectTransform>(), toReturn.mergeOrigin.GetComponent<RectTransform>());
            toReturn.connections.Add(connectionMerged);
            ConnectionManager.AddConnection(connectionMerged);
        }
        Connection connection = Instantiate<Connection>(connectionPrefab);
        connection.SetTargets(toReturnGO.GetComponent<RectTransform>(), toReturn.parent.GetComponent<RectTransform>());
        toReturn.connections.Add(connection);
        ConnectionManager.AddConnection(connection);

        return toReturn;
    }

    private void OnDestroy()
    {
        foreach (Connection connection in connections)
        {
            ConnectionManager.RemoveConnection(connection);
        }
    }

    private void Awake()
    {

    }
}