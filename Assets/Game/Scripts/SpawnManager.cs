using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<SpawnWave> _spawnWaves = new List<SpawnWave>();

    private List<GameObject> _spawnedEnemies = new List<GameObject>();

    private GameObject _spawnStorage;
    
    private int _wave = -1;

    private bool _isSpawnInProgress;

    private void Awake()
    {
        _spawnStorage = GameObject.FindWithTag("Enemies Spawn Storage");
    }

    private void Update()
    {
        if (_spawnStorage.transform.childCount == 0 && !_isSpawnInProgress)
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
                _spawnedEnemies.Add(spawnPoint.Spawn(_spawnStorage.transform));
            }
        }

        _isSpawnInProgress = false;
    }
}
