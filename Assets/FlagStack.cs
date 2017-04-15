using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FlagStack
{
    public List<Flag> flags;
    private static IDManager idManager;
    public IDManager idManagerPrefab;
    public int id;

    public FlagStack()
    {
        if (idManager == null)
            idManager = new IDManager();
        flags = new List<Flag>();
        this.id = idManager.GetNewID();
    }

    public void AddFlagAt(Flag flag, int position)
    {
        flags.Insert(position, flag);
    }

    public void AddFlagFront(Flag flag)
    {
        AddFlagAt(flag, 0);
    }

    public void AddFlagBack(Flag flag)
    {
        AddFlagAt(flag, flags.Count);
    }

    public override bool Equals(object obj)
    {
        return (obj as FlagStack).id == this.id;
    }

    public override int GetHashCode()
    {
        return id;
    }
}
