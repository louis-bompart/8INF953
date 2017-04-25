using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NodeData
{
    public MapSaveStateSerializable saveStateSerializable;
    [NonSerialized]
    public MapSaveStateSerializable saveState;
    //null if is root
    public NodeData parent;
    public NodeData mergeOrigin;
    public bool isAMerge;
    [NonSerialized]
    public Node node;
    public int depth;
    public bool isRoot;
    public int nbChild;

    public NodeData()
    {

    }

    public NodeData(MapSaveStateSerializable saveState, NodeData parent)
    {
        this.parent = parent;
        this.nbChild = 0;
        this.saveState = saveState;
        if (parent != null)
        {
            this.depth = parent.depth + 1;
            parent.nbChild++;
        }
        this.saveStateSerializable = new MapSaveStateSerializable(saveState);
        this.isRoot = false;
        this.isAMerge = false;
    }

    public static NodeData CreateMergeNode(MapSaveStateSerializable saveState, NodeData into, NodeData from)
    {
        NodeData toReturn = new NodeData(saveState, into);
        toReturn.depth = Mathf.Max(into.depth, from.depth) + 1;
        toReturn.nbChild = 0;
        toReturn.isAMerge = true;
        toReturn.mergeOrigin = from;
        into.nbChild++;
        return toReturn;
    }

    public static NodeData CreateRoot(MapSaveStateSerializable saveState)
    {
        NodeData toReturn = new NodeData(saveState, null);
        toReturn.isRoot = true;
        toReturn.nbChild = 0;
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

    internal static Node CreateFromData(NodeData nodeData)
    {
        GameObject toReturnGO = Instantiate(original);
        Node toReturn = toReturnGO.GetComponent<Node>();

        toReturn.isRoot = nodeData.isRoot;
        if (!toReturn.isRoot)
            toReturn.parent = nodeData.parent.GetNode();
        toReturn.data = nodeData;
        toReturn.isAMerge = nodeData.isAMerge;
        if (nodeData.isAMerge)
        {
            toReturn.isAMerge = nodeData.mergeOrigin.GetNode();
            ConnectionManager.CreateConnection(toReturnGO.GetComponent<RectTransform>(), toReturn.mergeOrigin.GetComponent<RectTransform>());
            Connection conn = ConnectionManager.FindConnection(toReturn.GetComponent<RectTransform>(), toReturn.mergeOrigin.GetComponent<RectTransform>());
            ConnectionPoint[] points = conn.points;
            conn.line.startWidth = 0.5f;
            conn.line.endWidth = 0.5f;
            points[conn.GetIndex(toReturnGO.GetComponent<RectTransform>())].direction = ConnectionPoint.ConnectionDirection.West;
            points[conn.GetIndex(toReturn.mergeOrigin.GetComponent<RectTransform>())].direction = ConnectionPoint.ConnectionDirection.East;
            for (int i = 0; i < points.Length; i++)
            {
                points[i].color = new Color(0.2f, 0.2f, 0.2f);
                Connection connection = new Connection();
            }
        }
        if (!nodeData.isRoot)
        {
            ConnectionManager.CreateConnection(toReturnGO.GetComponent<RectTransform>(), toReturn.parent.GetComponent<RectTransform>());
            Connection conn = ConnectionManager.FindConnection(toReturn.GetComponent<RectTransform>(), toReturn.parent.GetComponent<RectTransform>());
            ConnectionPoint[] points = conn.points;
            conn.line.startWidth = 0.5f;
            conn.line.endWidth = 0.5f;
            points[conn.GetIndex(toReturnGO.GetComponent<RectTransform>())].direction = ConnectionPoint.ConnectionDirection.West;
            points[conn.GetIndex(toReturn.parent.GetComponent<RectTransform>())].direction = ConnectionPoint.ConnectionDirection.East;
            for (int i = 0; i < points.Length; i++)
            {
                points[i].color = new Color(0.2f, 0.2f, 0.2f);
            }
        }
        return toReturn;
    }

    private void OnDestroy()
    {
        if (!isRoot)
        {
            ConnectionManager.RemoveConnection(ConnectionManager.FindConnection(GetComponent<RectTransform>(), parent.GetComponent<RectTransform>()));
            parent.data.nbChild--;
        }
        if (isAMerge)
            ConnectionManager.RemoveConnection(ConnectionManager.FindConnection(GetComponent<RectTransform>(), mergeOrigin.GetComponent<RectTransform>()));
    }

    private void Awake()
    {

    }
}