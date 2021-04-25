using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class InfiniteSpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPrefab;
    [SerializeField] private float _secondsSpawn;

    private float _spawnTimer;
    
    private void Update()
    {
        if (_spawnTimer == 0.0f)
        {
            Instantiate(_spawnPrefab, transform.position, transform.rotation);
            _spawnTimer = _secondsSpawn;
        }
        
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0.0f)
        {
            _spawnTimer = 0.0f;
        }
    }
}
