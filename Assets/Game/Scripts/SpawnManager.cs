using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<SpawnWave> _spawnWaves = new List<SpawnWave>();

    private List<GameObject> _spawnedEnemies = new List<GameObject>();

    private int _wave = -1;

    private bool _isSpawnInProgress;
    
    private void Update()
    {
        if (transform.childCount == 0 && !_isSpawnInProgress)
        {
            StartCoroutine(SpawnNextWave());
        }
    }

    private IEnumerator SpawnNextWave()
    {
        _isSpawnInProgress = true;
        
        _wave++;

        if (_wave < _spawnWaves.Count)
        {
            var spawnWave = _spawnWaves[_wave];

            yield return new WaitForSeconds(spawnWave.delay);
            
            foreach (var spawnPoint in spawnWave.spawnPoints)
            {
                _spawnedEnemies.Add(spawnPoint.Spawn(transform));
            }
        }

        _isSpawnInProgress = false;
    }
}
