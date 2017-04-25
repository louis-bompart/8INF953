using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStateManager : MonoBehaviour
{
    public NodeData root;
    public List<Tile> tiles;
    public int levelX;
    public int levelY;
    // Use this for initialization
    private void Awake()
    {
        //To Check : This or a generated grid "over" the game
        tiles = new List<Tile>(FindObjectsOfType<Tile>());
        Debug.Assert(tiles.Count == levelX * levelY, "Nb of tiles incorrect");
        MapSaveState rootState = MapSaveState.Create(levelY, levelY);
        root = NodeData.CreateRoot(rootState);
    }

    // Update is called once per frame
    void Update()
    {

    }
}



