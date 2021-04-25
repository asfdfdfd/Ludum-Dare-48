using UnityEngine;

public class PlayerShootController : MonoBehaviour
{
    [SerializeField] private GameObject _prefabFireball;
    [SerializeField] private float _shootCooldown;
    
    private bool _isFirePressed = false;

    private float _shootTimer;
    
    private void Update()
    {
        if (_shootTimer == 0.0f)
        {
            bool isFirePressedNew = Input.GetAxis("Fire1") > 0;

            if (_isFirePressed && !isFirePressedNew)
            {
                var gameObjectFireball = Instantiate(_prefabFireball);

                var projectileController = gameObjectFireball.GetComponent<ProjectileController>();

                var fireballStartLocation = transform.position;
                var fireballTargetLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                projectileController.LaunchTowards(fireballStartLocation, fireballTargetLocation);

                _shootTimer = _shootCooldown;
            }

            _isFirePressed = isFirePressedNew;
        }

        if (_shootTimer > 0)
        {
            _shootTimer -= Time.deltaTime;
        }
        else
        {
            _shootTimer = 0.0f;
        }
    }
}
