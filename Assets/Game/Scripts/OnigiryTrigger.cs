using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnigiryTrigger : MonoBehaviour
{
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
            Destroy(gameObject);
        }
    }
}
