using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NodeData
{
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
    public List<Node> children;
    public List<Node> mergeChildren;

    public NodeData()
    {

    }

    public NodeData(NodeData parent, MapSaveStateSerializable saveState = null)
    {
        this.parent = parent;
        this.nbChild = 0;
        if (parent != null)
        {
            this.depth = parent.depth + 1;
            parent.nbChild++;
        }
        if (saveState == null)
            this.saveState = new MapSaveStateSerializable(parent.saveState);
        else
            this.saveState = saveState;
        this.isRoot = false;
        this.isAMerge = false;
    }

    public static NodeData CreateMergeNode(NodeData into, NodeData from)
    {
        MapSaveStateSerializable saveState = new MapSaveStateSerializable(into, from);
        NodeData toReturn = new NodeData(null, saveState);
        toReturn.parent = from.depth > into.depth ? from : into;
        toReturn.mergeOrigin = from.depth > into.depth ? into : from;
        toReturn.depth = Mathf.Max(into.depth, from.depth) + 1;
        toReturn.nbChild = 0;
        toReturn.isAMerge = true;
        toReturn.parent.nbChild++;
        return toReturn;
    }

    public static NodeData CreateRoot(MapSaveStateSerializable saveState)
    {
        NodeData toReturn = new NodeData(null, saveState);
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
    public Color defaultColor;
    public Color selectedColor;
    public int height;
    public int childPosition;
    public List<Node> children;
    public List<Node> mergeChildren;

    internal static Node CreateFromData(NodeData nodeData)
    {
        GameObject toReturnGO = Instantiate(original);
        Node toReturn = toReturnGO.GetComponent<Node>();

        toReturn.isRoot = nodeData.isRoot;
        toReturn.children = new List<Node>();
        toReturn.mergeChildren = new List<Node>();
        toReturn.childPosition = 0;
        toReturn.height = 1;
        if (!toReturn.isRoot)
        {
            toReturn.parent = nodeData.parent.GetNode();
            toReturn.transform.SetParent(toReturn.parent.transform.parent, false);
            toReturn.GetComponent<RectTransform>().sizeDelta = toReturn.parent.GetComponent<RectTransform>().sizeDelta;
            toReturn.parent.children.Add(toReturn);
            toReturn.childPosition = toReturn.parent.height;
            NodeManager.instance.SetHeight(toReturn.parent);
        }
        toReturn.data = nodeData;
        toReturn.isAMerge = nodeData.isAMerge;
        if (nodeData.isAMerge)
        {
            toReturn.mergeOrigin = nodeData.mergeOrigin.GetNode();
            ConnectionManager.CreateConnection(toReturnGO.GetComponent<RectTransform>(), toReturn.mergeOrigin.GetComponent<RectTransform>());
            Connection conn = ConnectionManager.FindConnection(toReturn.GetComponent<RectTransform>(), toReturn.mergeOrigin.GetComponent<RectTransform>());
            ConnectionPoint[] points = conn.points;
            conn.line.startWidth = 0.5f;
            conn.line.endWidth = 0.5f;
            if (toReturnGO.GetComponent<RectTransform>().localPosition.y < toReturn.mergeOrigin.GetComponent<RectTransform>().localPosition.y)
            {
                points[conn.GetIndex(toReturnGO.GetComponent<RectTransform>())].direction = ConnectionPoint.ConnectionDirection.North;
            }
            else
            {
                points[conn.GetIndex(toReturnGO.GetComponent<RectTransform>())].direction = ConnectionPoint.ConnectionDirection.South;
            }
            points[conn.GetIndex(toReturn.mergeOrigin.GetComponent<RectTransform>())].direction = ConnectionPoint.ConnectionDirection.East;
            for (int i = 0; i < points.Length; i++)
            {
                points[i].color = new Color(0.2f, 0.2f, 0.2f);
                Connection connection = new Connection();
            }
            toReturn.mergeOrigin.mergeChildren.Add(toReturn);
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
        Connection toRemove;
        if (!isRoot)
        {
            toRemove = ConnectionManager.FindConnection(GetComponent<RectTransform>(), parent.GetComponent<RectTransform>());
            Destroy(toRemove.gameObject);
            parent.data.nbChild--;
        }
        if (isAMerge)
        {
            toRemove = ConnectionManager.FindConnection(GetComponent<RectTransform>(), mergeOrigin.GetComponent<RectTransform>());
            Destroy(toRemove.gameObject);
        }

        ConnectionManager.CleanConnections();
    }

    private void Awake()
    {
    }
    public void OnClick()
    {
        NodeManager.instance.SetCurrent(this);
    }
}