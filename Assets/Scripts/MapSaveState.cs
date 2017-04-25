using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSaveState : MonoBehaviour
{
    public static GameObject original;
    public int xSize;
    public int ySize;
    public int cellSize;
    public TileData[,] tiles;
    internal static MapSaveState current;

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

    internal static MapSaveState GetCopy(MapSaveState original)
    {
        MapSaveState toReturn = Instantiate(original).GetComponent<MapSaveState>();
        toReturn.xSize = original.xSize;
        toReturn.ySize = original.ySize;
        toReturn.tiles = original.tiles;
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

    public void CreateTiles()
    {
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                Tile tile = Tile.CreateTile();
                tile.transform.position = i * Vector3.forward * cellSize + j * Vector3.up * cellSize;
                tile.transform.rotation = transform.rotation;
                tiles[i, j] = tile.data;
            }
        }
    }

    public void LinkTilesWithData()
    {
        List<Tile> tilesGO = new List<Tile>(FindObjectsOfType<Tile>());
        Dictionary<int, Tile> refTilesGO = new Dictionary<int, Tile>();
        foreach (Tile tile in tilesGO)
        {
            refTilesGO.Add(tile.id, tile);
        }
        foreach (TileData data in tiles)
        {
            Tile tmp = null;
            if (refTilesGO.TryGetValue(data.id, out tmp))
                tmp.data = data;
            else
                Debug.Log("TileData without Tile, id:" + data.id);
        }
    }

    void Awake()
    {
        if (tiles == null)
            tiles = new TileData[xSize, ySize];
        CreateTiles();
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

    public MapSaveStateSerializable(MapSaveStateSerializable saveState)
    {
        this.xSize = saveState.xSize;
        this.ySize = saveState.ySize;
        int size = saveState.xSize * saveState.ySize;
        tiles = new TileData[saveState.tiles.Length];
        for (int i = 0; i < saveState.tiles.Length; i++)
        {
            tiles[i] = saveState.tiles[i];
        }
    }

    public MapSaveState GetMapSaveState()
    {
        if (mapSaveState == null)
            mapSaveState = MapSaveState.CreateFromSerialized(this);
        return mapSaveState;
    }

}
