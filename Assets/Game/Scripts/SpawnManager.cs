using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<SpawnWave> _spawnWaves = new List<SpawnWave>();

    private List<GameObject> _spawnedEnemies = new List<GameObject>();

    private int _wave = -1;

    private void Update()
    {
        if (transform.childCount == 0)
        {
            SpawnNextWave();
        }
    }

    private void SpawnNextWave()
    {
        _wave++;

        if (_wave < _spawnWaves.Count)
        {
            var spawnWave = _spawnWaves[_wave];

            foreach (var spawnPoint in spawnWave.spawnPoints)
            {
                _spawnedEnemies.Add(spawnPoint.Spawn(transform));
            }
        }
    }
}
