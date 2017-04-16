using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileSerializable
{
    string tileName;
    string description;
    bool isWalkable;
    bool isDeadly;
    private FlagStack flagStack;

    public FlagStack FlagStack
    {
        get
        {
            return flagStack;
        }
    }

    private TileSerializable()
    {
        flagStack = new FlagStack();
    }
}

public class Tile : MonoBehaviour
{

}

