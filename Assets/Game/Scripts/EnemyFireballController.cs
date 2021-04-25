using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireballController : MonoBehaviour
{
    [SerializeField] private float _damage;

    private GameObject _gameObjectPlayer;
    private PlayerDamageController _playerDamageController;
    private GameObject _gameObjectPlayerBody;
    private Collider2D _playerBodyCollider;
    
    private void Awake()
    {
        _gameObjectPlayer = GameObject.Find("Player");
        _playerDamageController = _gameObjectPlayer.GetComponent<PlayerDamageController>();
        _gameObjectPlayerBody = GameObject.Find("Player Body");
        _playerBodyCollider = _gameObjectPlayerBody.GetComponent<Collider2D>();
    }

    public void OnFireballTriggerEnter(Collider2D other)
    {
        if (other == _playerBodyCollider)
        {
            _playerDamageController.Damage(_damage);
            Destroy(gameObject);
            return;
        }
        
        var block = other.GetComponent<Block>();
        if (block != null)
        {
            Destroy(gameObject);
            return;
        }
    }
}
