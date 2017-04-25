﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileData
{
    internal FlagStack flagStack;
    internal int id;
    bool isLocked;

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
        isLocked = false;
        flagStack = new FlagStack();
    }

    public Tile GetTile()
    {
        return Tile.mappedTile[id];
    }
}

public class Tile : MonoBehaviour
{
    public static GameObject original;
    public static GameObject cursor;
    public static Dictionary<int, Tile> mappedTile;
    private static IDManager idManager;
    public List<Flag> currentFlagState;
    public TileData data;
    internal string tileName;
    internal string description;
    internal bool isWalkable;
    internal bool isDeadly;
    internal int id;

    public static Tile CreateTile()
    {
        Tile toReturn = Instantiate(original).GetComponent<Tile>();
        if (idManager == null)
            idManager = new IDManager();
        if (mappedTile == null)
            mappedTile = new Dictionary<int, Tile>();
        if (cursor == null)
        {
            cursor = GameObject.FindWithTag("Cursor");
            cursor.SetActive(false);
        }
        toReturn.id = idManager.GetNewID();
        toReturn.data = new TileData(toReturn.id);
        toReturn.currentFlagState = new List<Flag>(toReturn.data.flagStack.flags);
        toReturn.AddNextFlag();
        mappedTile.Add(toReturn.id, toReturn);
        return toReturn;
    }

    private void Awake()
    {
    }

    public void AddNextFlag()
    {
        if (currentFlagState.Count > 0)
        {
            GameObject newFlag = Instantiate(currentFlagState[0].gameObject);
            newFlag.transform.SetParent(transform, false);
            newFlag.transform.localPosition = Vector3.zero;
            currentFlagState.RemoveAt(0);
            newFlag.GetComponent<Flag>().SetListener(this);
        }
    }

    public void RefreshFlagStack()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag != "Cursor")
                Destroy(transform.GetChild(i).gameObject);
        }
        currentFlagState = new List<Flag>(data.flagStack.flags);
        AddNextFlag();
    }

    private void OnMouseEnter()
    {
        cursor.transform.SetParent(transform, transform);
        cursor.transform.localPosition = Vector3.zero;
        cursor.SetActive(true);
    }
    private void OnMouseUpAsButton()
    {
        SlotManager.instance.LoadFlagStack(this);
    }
}

