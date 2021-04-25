using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    
    private Vector2 _direction;
    
    private bool _isLaunched;

    private float _speed;
    
    public void LaunchTowards(Vector2 fireballStartLocation, Vector2 fireballTargetLocation)
    {
        _speed = moveSpeed;
        
        transform.position = fireballStartLocation;
        
        _direction = (fireballTargetLocation - fireballStartLocation).normalized;

        _isLaunched = true;
    }

    private void FixedUpdate()
    {
        if (_isLaunched)
        {
            transform.Translate(_direction * _speed * Time.deltaTime);

            if (Mathf.Abs(transform.position.x) > 30 || Mathf.Abs(transform.position.y) > 30)
            {
                Destroy(gameObject);
            }
        }
    }
}
