using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToTheLevel : MonoBehaviour
{
    [SerializeField] private string _levelName;
    
    private Collider2D _playerCollider;

    private void Start()
    {
        var gameObjectPlayer = GameObject.Find("Player");
        _playerCollider = gameObjectPlayer.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_playerCollider == other)
        {
            GameState.HealthOnLevelStart = GameState.CurrentHealth;
            
            if (GameState.OnigiryEaten)
            {
                GameState.OnigiryEaten = false;
                GameState.CurrentLevelName = _levelName;
                GameState.PreviousLevelName = null;
                SceneManager.LoadScene(_levelName);
            }
            else
            {
                GameState.CurrentLevelName = GameState.PreviousLevelName;
                GameState.PreviousLevelName = null;
                SceneManager.LoadScene(GameState.CurrentLevelName);
            }
        }
    }
}
