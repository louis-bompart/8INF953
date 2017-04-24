using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public GameObject mapSaveStatePrefab;
    public GameObject tilePrefab;
    public GameObject slotPrefab;
    public List<GameObject> itemPrefabs;
    private void Awake()
    {
        if (FindObjectsOfType<Initializer>().Length > 1)
            DestroyImmediate(gameObject);
        else
        {
            MapSaveState.original = mapSaveStatePrefab;
            Tile.original = tilePrefab;
            Item.itemsPrefab = itemPrefabs;
            Slot.original = slotPrefab;
            //Add other init needed here
        }
    }
}
