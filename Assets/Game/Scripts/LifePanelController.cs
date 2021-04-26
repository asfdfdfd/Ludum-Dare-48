using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePanelController : MonoBehaviour
{
    private PlayerDamageController _playerDamageController;

    [SerializeField] private GameObject _prefabLife;

    private List<ImageLifeController> _gameObjects = new List<ImageLifeController>();
    
    private void Start()
    {
        var gameObjectPlayer = GameObject.Find("Player");
        
        _playerDamageController = gameObjectPlayer.GetComponent<PlayerDamageController>();

        for (int i = 0; i < _playerDamageController.MaxHealth; i++)
        {
            var gameObjectLife = Instantiate(_prefabLife);
            gameObjectLife.transform.parent = gameObject.transform;
            _gameObjects.Add(gameObjectLife.GetComponent<ImageLifeController>());
        }
    }

    private void Update()
    {
        for (int i = 0; i < GameState.CurrentHealth; i++)
        {
            _gameObjects[i].SetLifeFull();
        }

        for (int i = (int)GameState.CurrentHealth; i < _playerDamageController.MaxHealth; i++)
        {
            _gameObjects[i].SetLifeEmpty();
        }
    }
}
