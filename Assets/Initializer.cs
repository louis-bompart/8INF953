﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public GameObject mapSaveStatePrefab;

    private void Awake()
    {
        if (FindObjectsOfType<Initializer>().Length > 1)
            DestroyImmediate(gameObject);
        else
        {
            MapSaveState.original = mapSaveStatePrefab;
            //Add other init needed here
        }
    }
}