using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public Node Merge(Node from, Node into)
    {
        //ToDo Handle conflicts;
        return NodeData.CreateMergeNode(null, into.data, from.data).GetNode();
    }

    Node root;
    MapSaveState initalSaveState;
    Node current;
    List<Node> nodes;
    private void Awake()
    {
        //ToDo set initialSaveState;
        nodes = new List<Node>();
        root = NodeData.CreateRoot(initalSaveState).GetNode();
        current = root;
    }

    public Node CreateChild(Node parent)
    {
        Node toReturn = new NodeData(MapSaveState.GetCopy(parent.data.saveState), parent.data).GetNode();
        return toReturn;
    }

    public void DeleteNode(Node toDel)
    {
        Queue<Node> children = new Queue<Node>(nodes.FindAll(node => node.parent == toDel));
        while (children.Count>0)
        {
            DeleteNode(children.Dequeue());
        }
        DestroyImmediate(toDel);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
