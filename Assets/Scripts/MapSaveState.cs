using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSaveState : MonoBehaviour
{
    public static GameObject original;
    public int xSize;
    public int ySize;
    public Tile[,] tiles;

    // Use this for initialization
    public static MapSaveState CreateFromSerialized(MapSaveStateSerializable serialized)
    {
        MapSaveState toReturn;
        toReturn = Instantiate(original).GetComponent<MapSaveState>();
        toReturn.xSize = serialized.xSize;
        toReturn.ySize = serialized.ySize;
        return toReturn;
    }

    void Start()
    {
        if (tiles == null)
            tiles = new Tile[xSize, ySize];
    }
}

[Serializable]
public class MapSaveStateSerializable
{
    public int xSize;
    public int ySize;
    [NonSerialized]
    public MapSaveState mapSaveState;
    [NonSerialized]
    public int size;
    public Tile[] tiles;

    public MapSaveStateSerializable()
    {
    }

    public MapSaveStateSerializable(MapSaveState saveState)
    {
        xSize = saveState.xSize;
        ySize = saveState.ySize;

        size = saveState.xSize * saveState.ySize;
        tiles = new Tile[size];
        for (int i = 0; i < saveState.xSize; i++)
        {
            for (int j = 0; j < saveState.ySize; j++)
            {
                tiles[i + j * saveState.xSize] = saveState.tiles[i, j];
            }
        }
    }

    public MapSaveState GetMapSaveState()
    {
        if (mapSaveState == null)
            mapSaveState = MapSaveState.CreateFromSerialized(this);
        return mapSaveState;
    }

}
