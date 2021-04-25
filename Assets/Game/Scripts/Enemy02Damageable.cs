using UnityEngine;

public class Enemy02Damageable : MonoBehaviour, IDamageable
{
    [SerializeField] private Enemy02Controller _enemyController;
    
    public void Damage(float damage)
    {
        _enemyController.Damage(damage);
    }
}
