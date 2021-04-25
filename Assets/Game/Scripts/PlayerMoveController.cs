using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _stopLinearDrag;
    [SerializeField] private Rigidbody2D _playerRigidbody;

    private bool _isMovementEnabled = true;
    
    private void FixedUpdate()
    {
        if (!_isMovementEnabled)
        {
            return;
        }
        
        var movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        movementVector = Vector2.ClampMagnitude(movementVector, 1.0f);
        
        if (movementVector.Equals(Vector2.zero))
        {
            _playerRigidbody.drag = _stopLinearDrag;
        }
        else
        {
            _playerRigidbody.drag = 0.0f;
        }

        _playerRigidbody.velocity = movementVector * (_playerSpeed * Time.deltaTime);
    }

    public void DisableMovement()
    {
        _isMovementEnabled = false;

        _playerRigidbody.drag = 0.0f;
        _playerRigidbody.velocity = Vector2.zero;
    }

    public void EnableMovement()
    {
        _isMovementEnabled = true;
        
        _playerRigidbody.drag = 0.0f;
        _playerRigidbody.velocity = Vector2.zero;        
    }
}
