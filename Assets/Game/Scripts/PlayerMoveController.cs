using System;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _stopLinearDrag;
    [SerializeField] private Rigidbody2D _playerRigidbody;
    [SerializeField] private Animator _animator;
    
    private bool _isMovementEnabled = true;
    
    private int _animationHashIsWalking;

    private void Awake()
    {
        _animationHashIsWalking = Animator.StringToHash("IsWalking");
    }

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
            
            _animator.SetBool(_animationHashIsWalking, false);
        }
        else
        {
            _playerRigidbody.drag = 0.0f;
            
            _animator.SetBool(_animationHashIsWalking, true);
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
