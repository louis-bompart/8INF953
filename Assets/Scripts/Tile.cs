using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    [SerializeField]
    string tileName;
    [SerializeField]
    string description;
    [SerializeField]
    bool isWalkable;
    [SerializeField]
    bool isDeadly;
    [SerializeField]
    private FlagStack flagStack;
    [SerializeField]
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
