using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnigiryTrigger : MonoBehaviour
{
    private Collider2D _playerCollider;
    private PlayerDamageController _playerDamageController;
    
    private void Start()
    {
        var gameObjectPlayer = GameObject.Find("Player");
        _playerCollider = gameObjectPlayer.GetComponent<Collider2D>();
        _playerDamageController = gameObjectPlayer.GetComponent<PlayerDamageController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_playerCollider == other)
        {
            GameState.OnigiryEaten = true;

            GameState.CurrentHealth = _playerDamageController.MaxHealth;
            
            Destroy(gameObject);
        }
    }
}
