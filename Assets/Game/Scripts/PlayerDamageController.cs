using UnityEngine;

public class PlayerDamageController : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    private float _health;

    private void Awake()
    {
        _health = _maxHealth;
    }

    public bool Damage(float damage)
    {
        _health -= damage;

        if (_health <= 0.0f)
        {
            Destroy(gameObject);
            return false;
        }

        return true;
    }
}
