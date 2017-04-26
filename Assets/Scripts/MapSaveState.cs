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
    public MapSaveStateSerializable serializable;

    public static bool onlyOnce = true;

    // Use this for initialization
    public static MapSaveState CreateFromSerialized(MapSaveStateSerializable serialized)
    {
        //MapSaveState toReturn = Create(serialized.xSize, serialized.ySize);
        MapSaveState toReturn = Instantiate(original).GetComponent<MapSaveState>();
        toReturn.xSize = serialized.xSize;
        toReturn.ySize = serialized.ySize;
        toReturn.tiles = new TileData[toReturn.xSize, toReturn.ySize];

        for (int i = 0; i < serialized.tiles.Length; i++)
        {
            toReturn.tiles[i % toReturn.xSize, i / toReturn.xSize] = serialized.tiles[i].GetCopy();
        }
        toReturn.serializable = serialized;
        toReturn.LinkTilesWithData();
        return toReturn;
    }

    //internal static MapSaveState GetCopy(MapSaveState original)
    //{
    //    MapSaveState toReturn = Instantiate(original).GetComponent<MapSaveState>();
    //    toReturn.xSize = original.xSize;
    //    toReturn.ySize = original.ySize;
    //    toReturn.tiles = original.tiles;
    //    return toReturn;
    //}

    public static MapSaveState Create(int xSize, int ySize)
    {
        MapSaveState toReturn = Instantiate(original).GetComponent<MapSaveState>();
        toReturn.xSize = xSize;
        toReturn.ySize = ySize;
        toReturn.tiles = new TileData[toReturn.xSize, toReturn.ySize];
        toReturn.serializable = new MapSaveStateSerializable(toReturn);
        //toReturn.CreateTiles();
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
            {
                tmp.data = data;
                tmp.RefreshFlagStack();
            }
            else
                Debug.Log("TileData without Tile, id:" + data.id);
        }
    }

    void Awake()
    {

        if (tiles == null)
            tiles = new TileData[xSize, ySize];
        //CreateTiles();
        if (onlyOnce)
        {
            onlyOnce = false;
            CreateTiles();
        }
        current = this;
    }
}

[Serializable]
public class MapSaveStateSerializable
{
    public int xSize;
    public int ySize;
    public TileData[] tiles;

	public CheckPointData initCheck;
	public CheckPointData finalCheck;

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
        this.mapSaveState = saveState;
        this.mapSaveState.serializable = this;
    }

    public void Update(MapSaveState saveState)
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

    public MapSaveStateSerializable(NodeData into, NodeData from)
    {
        this.xSize = into.saveState.xSize;
        this.ySize = into.saveState.ySize;
        int size = into.saveState.xSize * into.saveState.ySize;
        tiles = new TileData[size];
        NodeData used = into;
        if (from.depth > into.depth)
        {
            used = from;
        }
        for (int i = 0; i < size; i++)
        {
            tiles[i] = used.saveState.tiles[i];
            if (from.saveState.tiles[i].isLocked || into.saveState.tiles[i].isLocked)
                tiles[i].isLocked = true;
            if (!from.saveState.tiles[i].Equals(into.saveState.tiles[i]))
            {
                tiles[i].isLocked = true;
            }
        }
    }


    public MapSaveState GetMapSaveState()
    {
        if (this.mapSaveState == null)
            this.mapSaveState = MapSaveState.CreateFromSerialized(this);
        return this.mapSaveState;
    }

}
