using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public GameObject mapSaveStatePrefab;
    public GameObject tilePrefab;
    public GameObject slotPrefab;
    public GameObject nodePrefab;
    public List<GameObject> itemPrefabs;
    public List<GameObject> flagPrefabs;
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
            Node.original = nodePrefab;
            //Add other init needed here
        }
    }
}
