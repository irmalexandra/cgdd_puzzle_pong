using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject toSpawn;
    public int allowedAmount;
    private List<GameObject> _currentSpawns;


    private void Start()
    {
        _currentSpawns = new List<GameObject>();
    }

    public void Spawn()
    {
        GameObject spawn = Instantiate(toSpawn, transform);
        _currentSpawns.Add(spawn);
        if (_currentSpawns.Count > allowedAmount)
        {
            Destroy(_currentSpawns[0]);
            _currentSpawns.RemoveAt(0);
        }
    }
}
