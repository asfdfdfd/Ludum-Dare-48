using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.AI;

public class Enemy01Controller : MonoBehaviour
{
    [SerializeField] private float _secondsPauseBeforeAttack;
    [SerializeField] private float _secondsPauseAfterAttack;
    [SerializeField] private float _secondsPlayerStun;
    [SerializeField] private float _pushForce;
    [SerializeField] private float _maxHealth;
    
    private GameObject _gameObjectPlayer;
    private Rigidbody2D _playerRigidbody;
    private GameObject _gameObjectPlayerBody;
    private Collider2D _playerBodyCollider;
    private PlayerMoveController _playerMoveController;
    private NavMeshAgent _navMeshAgent;
    
    [SerializeField] private Collider2D _attackTrigger;

    private Rigidbody2D _rigidbody2D;

    private bool _isAttackStarted;

    private bool _shouldAttack;

    private float _health;
    
    private void Awake()
    {
        _health = _maxHealth;
        
        _gameObjectPlayer = GameObject.Find("Player");
        _playerRigidbody = _gameObjectPlayer.GetComponent<Rigidbody2D>();
        _playerMoveController = _gameObjectPlayer.GetComponent<PlayerMoveController>();
        
        _gameObjectPlayerBody = GameObject.Find("Player Body");
        _playerBodyCollider = _gameObjectPlayerBody.GetComponent<Collider2D>();
        
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

    private void FixedUpdate()
    {
        if (!_isAttackStarted && !_shouldAttack && _gameObjectPlayer != null)
        {
            _navMeshAgent.destination = _gameObjectPlayer.transform.position;
        }
        else if (!_isAttackStarted && _shouldAttack)
        {
            _isAttackStarted = true;

            _navMeshAgent.destination = gameObject.transform.position;
            
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(_secondsPauseBeforeAttack);

        if (_shouldAttack)
        {
            _playerMoveController.DisableMovement();
            
            var collisionPoint = _attackTrigger.ClosestPoint(_gameObjectPlayerBody.transform.position);

            var force = (collisionPoint - (Vector2)transform.position).normalized;

            _playerRigidbody.drag = 10.0f;
            _playerRigidbody.AddForce(force * _pushForce, ForceMode2D.Impulse);

            yield return new WaitForSeconds(_secondsPlayerStun);
            
            _playerMoveController.EnableMovement();
        }

        yield return new WaitForSeconds(_secondsPauseAfterAttack);
        
        _isAttackStarted = false;
    }

    public void OnAttackTriggerEnter(Collider2D other)
    {
        if (_playerBodyCollider == other)
        {
            _shouldAttack = true;
        }
    }

    public void OnAttackTriggerExit(Collider2D other)
    {
        if (_playerBodyCollider == other)
        {
            _shouldAttack = false;
        }
    }

    public void Damage(float damage)
    {
        _health -= damage;

        if (_health <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
