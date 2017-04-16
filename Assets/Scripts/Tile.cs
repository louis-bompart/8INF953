using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tile
{
    string tileName;
    string description;
    bool isWalkable;
    bool isDeadly;
    private FlagStack flagStack;
    private IDManager idManager;
    public IDManager idManagerPrefab;

    public FlagStack FlagStack
    {
        get
        {
            return flagStack;
        }
    }

    private Tile()
    {
        if (idManager == null)
            idManager = new IDManager();
        flagStack = new FlagStack();
    }
}
