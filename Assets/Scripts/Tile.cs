using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
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
    TileData data;
    internal string tileName;
    internal string description;
    internal bool isWalkable;
    internal bool isDeadly;
    static IDManager idManager;
    internal int id;

    private void Awake()
    {
        idManager = new IDManager();
        id = idManager.GetNewID();
        data = new TileData(id);
    }
}

