using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeManager : MonoBehaviour
{
    public static NodeManager instance;
    Node root;
    MapSaveState initalSaveState;
    public Node current;
    //List<Node> nodes;
    public Vector2 nodeSize;
    public float margin;

    private void Awake()
    {
        initalSaveState = FindObjectOfType<MapSaveState>();
        //ToDo check initialSaveState;
        //  Certainly need to load it from files, when the initialState'd been designed
        //nodes = new List<Node>();
        root = NodeData.CreateRoot(new MapSaveStateSerializable(initalSaveState)).GetNode();
        root.transform.SetParent(transform, false);
        root.GetComponent<RectTransform>().sizeDelta = nodeSize;
        root.GetComponent<RectTransform>().anchoredPosition3D = Vector3.Scale(new Vector3(nodeSize.x / 2 + margin, GetComponent<RectTransform>().rect.height / 2 - nodeSize.y / 2 - margin, 0), root.GetComponent<RectTransform>().localScale);
        SetCurrent(root);
        instance = this;
    }

    public void TestCreate()
    {
        CreateChild(current);
        SetCurrent(CreateChild(current));
    }

    public Node CreateChild(Node parent)
    {
        Node toReturn = new NodeData(parent.data).GetNode();
        //toReturn.GetComponent<RectTransform>().anchoredPosition3D = parent.GetComponent<RectTransform>().anchoredPosition3D + Vector3.Scale(Vector3.right * (nodeSize.x + margin) + Vector3.down * (nodeSize.y + margin) * (parent.data.nbChild - 1), toReturn.GetComponent<RectTransform>().localScale);
        //nodes.Add(toReturn);
        return toReturn;
    }

    public void DeleteNode(Node toDel)
    {
        if (!toDel.isRoot)
            toDel.parent.children.Remove(toDel);
        if (toDel.isAMerge)
            toDel.mergeOrigin.mergeChildren.Remove(toDel);
        Queue<Node> children = new Queue<Node>(toDel.children);
        while (children.Count > 0)
        {
            DeleteNode(children.Dequeue());
        }
        children = new Queue<Node>(toDel.mergeChildren);
        while (children.Count > 0)
        {
            DeleteNode(children.Dequeue());
        }
        if (toDel != null)
            Destroy(toDel.gameObject);
    }

    public Node Merge(Node from, Node into)
    {
        Node toReturn = NodeData.CreateMergeNode(into.data, from.data).GetNode();
        toReturn.transform.SetParent(transform, false);
        toReturn.GetComponent<RectTransform>().sizeDelta = nodeSize;
        //toReturn.GetComponent<RectTransform>().anchoredPosition3D = toReturn.parent.GetComponent<RectTransform>().anchoredPosition3D + Vector3.Scale(Vector3.right * (nodeSize.x + margin) + Vector3.down * (nodeSize.y + margin) * (toReturn.parent.data.nbChild - 1), toReturn.GetComponent<RectTransform>().localScale);
        //ToDo put int the avg height of from and into
        //toReturn.GetComponent<RectTransform>().localPosition = from.GetComponent<RectTransform>().localPosition
        //nodes.Add(toReturn);

        return toReturn;
        //ToDo Handle conflicts;
    }
    internal void SetHeight(Node parent)
    {
        int height = 0;
        for (int i = 0; i < parent.children.Count; i++)
        {
            height += parent.children[i].height;
        }
        parent.height = height;
        if (!parent.isRoot)
            SetHeight(parent.parent);
        else
            ReWorkNodePosition(root);
    }

    private void ReWorkNodePosition(Node node)
    {
        int usedHeight = 0;
        for (int i = 0; i < node.children.Count; i++)
        {
            node.children[i].GetComponent<RectTransform>().anchoredPosition3D = node.children[i].parent.GetComponent<RectTransform>().anchoredPosition3D + Vector3.Scale(Vector3.right * (nodeSize.x + margin) + Vector3.down * (nodeSize.y + margin) * (usedHeight), node.children[i].GetComponent<RectTransform>().localScale);
            ReWorkNodePosition(node.children[i]);
            usedHeight += node.children[i].height;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void SetCurrent(Node node)
    {
        if (mergeOnGoing)
        {
            mergeOnGoing = false;
            SetCurrent(Merge(fromTMP, node));
            return;
        }
        if (current != null)
            current.GetComponent<Image>().color = current.defaultColor;
        current = node;
        current.GetComponent<Image>().color = current.selectedColor;
    }

    public void AddChild()
    {
        SetCurrent(CreateChild(current));
    }

    public void DisownIndividual()
    {
        if (current.isRoot)
        {
            //Display a warning to user, and do nothing
            return;
        }
        Node selected = current.parent;
        DeleteNode(current);
        SetCurrent(selected);
    }
    private Node fromTMP;
    private bool mergeOnGoing;
    public void Join()
    {

        if (mergeOnGoing)
        {
            fromTMP = null;
        }
        else
        {
            fromTMP = current;
        }
        mergeOnGoing = !mergeOnGoing;

    }
}
