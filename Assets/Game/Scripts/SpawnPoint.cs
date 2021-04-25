using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject _prefabEnemy;

    public GameObject Spawn(Transform parent)
    {
        var gameObject = Instantiate(_prefabEnemy, transform.position, Quaternion.identity);
        gameObject.transform.parent = parent;
        return gameObject;
    }
}
