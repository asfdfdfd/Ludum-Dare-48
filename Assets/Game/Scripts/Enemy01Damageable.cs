using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Damageable : MonoBehaviour, IDamageable
{
    [SerializeField] private Enemy01Controller _enemyController;
    
    public void Damage(float damage)
    {
        _enemyController.Damage(damage);
    }
}
