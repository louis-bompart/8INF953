using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeManager : MonoBehaviour
{
    Node root;
    MapSaveState initalSaveState;
    Node current;
    List<Node> nodes;
    public Vector2 nodeSize;
    public float margin;

    private void Awake()
    {
        initalSaveState = FindObjectOfType<MapSaveState>();
        //ToDo check initialSaveState;
        //  Certainly need to load it from files, when the initialState'd been designed
        nodes = new List<Node>();
        root = NodeData.CreateRoot(new MapSaveStateSerializable(initalSaveState)).GetNode();
        root.transform.SetParent(transform, false);
        root.GetComponent<RectTransform>().sizeDelta = nodeSize;
        root.GetComponent<RectTransform>().anchoredPosition3D = Vector3.Scale(new Vector3(nodeSize.x / 2 + margin, GetComponent<RectTransform>().rect.height / 2 - nodeSize.y / 2 - margin, 0), root.GetComponent<RectTransform>().localScale);
        current = root;
    }

    public void TestCreate()
    {
        CreateChild(current);
        current = CreateChild(current);
    }

    public Node CreateChild(Node parent)
    {
        Node toReturn = new NodeData(parent.data.saveState, parent.data).GetNode();
        toReturn.transform.SetParent(transform, false);
        toReturn.GetComponent<RectTransform>().sizeDelta = nodeSize;
        toReturn.GetComponent<RectTransform>().anchoredPosition3D = parent.GetComponent<RectTransform>().anchoredPosition3D + Vector3.Scale(Vector3.right * (nodeSize.x + margin) + Vector3.down * (nodeSize.y + margin) * (parent.data.nbChild-1), toReturn.GetComponent<RectTransform>().localScale);
        return toReturn;
    }

    public void DeleteNode(Node toDel)
    {
        Queue<Node> children = new Queue<Node>(nodes.FindAll(node => node.parent == toDel));
        while (children.Count > 0)
        {
            DeleteNode(children.Dequeue());
        }
        DestroyImmediate(toDel);
    }

    public Node Merge(Node from, Node into)
    {
        Node toReturn = NodeData.CreateMergeNode(null, into.data, from.data).GetNode();
        toReturn.transform.SetParent(transform, false);
        toReturn.GetComponent<RectTransform>().sizeDelta = nodeSize;
        //ToDo put int the avg height of from and into
        //toReturn.GetComponent<RectTransform>().localPosition = from.GetComponent<RectTransform>().localPosition
        return toReturn;
        //ToDo Handle conflicts;
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
