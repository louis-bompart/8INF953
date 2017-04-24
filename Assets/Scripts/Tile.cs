using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileData
{
    internal FlagStack flagStack;
    internal int id;

    public FlagStack FlagStack
    {
        get
        {
            return flagStack;
        }
    }

    public TileData()
    {
    }

    public TileData(int id)
    {
        this.id = id;
        flagStack = new FlagStack();
    }
}

public class Tile : MonoBehaviour
{
    public static GameObject original;
    public static GameObject cursor;
    public TileData data;
    internal string tileName;
    internal string description;
    internal bool isWalkable;
    internal bool isDeadly;
    static IDManager idManager;
    internal int id;

    public static Tile CreateTile()
    {
        Tile toReturn = Instantiate(original).GetComponent<Tile>();
        if (idManager == null)
            idManager = new IDManager();
        if (cursor == null)
        {
            cursor = GameObject.FindWithTag("Cursor");
            cursor.SetActive(false);
        }
        toReturn.id = idManager.GetNewID();
        toReturn.data = new TileData(toReturn.id);
        return toReturn;
    }

    private void Awake()
    {

    }

    private void OnMouseEnter()
    {
        cursor.transform.SetParent(transform, transform);
        cursor.transform.localPosition = Vector3.zero;
        cursor.SetActive(true);
    }
}

