using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamageController : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    
    public float MaxHealth => _maxHealth;

    private void Awake()
    {
        if (!GameState.CurrentHealthInitialized)
        {
            GameState.CurrentHealthInitialized = true;
            GameState.CurrentHealth = _maxHealth;
            GameState.HealthOnLevelStart = _maxHealth;
        }

        if (GameState.CurrentLevelName == null)
        {
            GameState.CurrentLevelName = SceneManager.GetActiveScene().name;
        }
    }

    public bool Damage(float damage)
    {
        GameState.CurrentHealth -= damage;

        if (GameState.CurrentHealth <= 0.0f)
        {
            GameState.CurrentHealth = GameState.HealthOnLevelStart;
            
            SceneManager.LoadScene(GameState.CurrentLevelName);
            
            return false;
        }

        return true;
    }
}
