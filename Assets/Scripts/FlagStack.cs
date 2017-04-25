using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FlagStack
{
    public static Dictionary<int, FlagStack> mapFlagStack;
    public List<Flag> flags;
    private static IDManager idManager;
    public IDManager idManagerPrefab;
    public int id;

    public FlagStack()
    {
        if (mapFlagStack == null)
            mapFlagStack = new Dictionary<int, FlagStack>();
        if (idManager == null)
            idManager = new IDManager();
        flags = new List<Flag>();
        this.id = idManager.GetNewID();
        mapFlagStack.Add(this.id, this);
    }

    public FlagStack(FlagStack flagStack)
    {
        if (mapFlagStack == null)
            mapFlagStack = new Dictionary<int, FlagStack>();
        if (idManager == null)
            idManager = new IDManager();
        this.id = idManager.GetNewID();
        this.flags = new List<Flag>(flagStack.flags);
        mapFlagStack.Add(this.id, this);
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

    public void SwapFlag(int position1, int position2)
    {
        if (position2 == flags.Count && position1 < flags.Count)
        {
            AddFlagBack(flags[position1]);
            RemoveFlagAt(position1);
            return;
        }
        if (position1 == flags.Count && position2 < flags.Count)
        {
            AddFlagBack(flags[position2]);
            RemoveFlagAt(position2);
            return;
        }

        Flag flag1 = flags[position1];
        flags[position1] = flags[position2];
        flags[position2] = flag1;
        //TODO Maybe handle the swap between an empty case and a full one.
    }

    internal static FlagStack GetFS(FlagStack flagStack)
    {
        return mapFlagStack[flagStack.id];
    }

    public void RemoveFlagAt(int position)
    {
        flags.RemoveAt(position);
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
