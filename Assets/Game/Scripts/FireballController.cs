using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    [SerializeField] private float _damage;

    public void OnFireballTriggerEnter(Collider2D other)
    {
        var damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(_damage);
            Destroy(gameObject);
        }
    }
}
