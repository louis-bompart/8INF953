using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    Node Merge(Node from, Node into)
    {
        //ToDo Handle conflicts;
        return NodeData.CreateMergeNode(null, into.data, from.data).GetNode();
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
