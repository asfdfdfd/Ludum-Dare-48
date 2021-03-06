using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<SpawnWave> _spawnWaves = new List<SpawnWave>();

    private List<GameObject> _spawnedEnemies = new List<GameObject>();

    private GameObject _spawnStorage;
    
    private int _wave = -1;

    private bool _isSpawnInProgress;
    
    [SerializeField] private string _nextScene;
    
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
        else if (_nextScene != null)
        {
            var gameObjectMusicPlayer = GameObject.Find("MusicPlayer");
            if (gameObjectMusicPlayer != null)
            {
                var musicPlayer = gameObjectMusicPlayer.GetComponent<MusicPlayer>();
                musicPlayer.ReduceSound();
            }
            GameState.PreviousLevelName = GameState.CurrentLevelName;
            GameState.CurrentLevelName = _nextScene;
            GameState.HealthOnLevelStart = GameState.CurrentHealth;
            SceneManager.LoadScene(_nextScene);
        }

        _isSpawnInProgress = false;
    }
}
