using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSaveState : MonoBehaviour
{
    public static GameObject original;
    public int xSize;
    public int ySize;
    public TileData[,] tiles;

    // Use this for initialization
    public static MapSaveState CreateFromSerialized(MapSaveStateSerializable serialized)
    {
        MapSaveState toReturn = Create(serialized.xSize, serialized.ySize);
        for (int i = 0; i < serialized.tiles.Length; i++)
        {
            toReturn.tiles[i % toReturn.xSize, i / toReturn.xSize] = serialized.tiles[i];
        }
        return toReturn;
    }

    public static MapSaveState Create(int xSize, int ySize)
    {
        MapSaveState toReturn = Instantiate(original).GetComponent<MapSaveState>();
        toReturn.xSize = xSize;
        toReturn.ySize = ySize;
        toReturn.tiles = new TileData[toReturn.xSize, toReturn.ySize];
        return toReturn;
    }

    void Start()
    {
        if (tiles == null)
            tiles = new TileData[xSize, ySize];
    }
}

[Serializable]
public class MapSaveStateSerializable
{
    public int xSize;
    public int ySize;
    public TileData[] tiles;

    [NonSerialized]
    public MapSaveState mapSaveState;

    public MapSaveStateSerializable()
    {
    }

    public MapSaveStateSerializable(MapSaveState saveState)
    {
        xSize = saveState.xSize;
        ySize = saveState.ySize;

        int size = saveState.xSize * saveState.ySize;
        tiles = new TileData[size];
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
