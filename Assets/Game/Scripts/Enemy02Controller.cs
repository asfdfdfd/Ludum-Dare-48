using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.AI;

public class Enemy02Controller : MonoBehaviour
{
    [SerializeField] private float _secondsPauseBeforeAttack;
    [SerializeField] private float _secondsPauseAfterAttack;
    [SerializeField] private float _maxHealth;
    [SerializeField] private bool _shouldAlwaysAttack;
    [SerializeField] private bool _shouldMove;
    
    [SerializeField] private GameObject _prefabFireball;
    
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
        if (_shouldMove && !_isAttackStarted && !_shouldAttack && _gameObjectPlayer != null)
        {
            _navMeshAgent.destination = _gameObjectPlayer.transform.position;
        }
        else if (!_isAttackStarted && (_shouldAttack || _shouldAlwaysAttack))
        {
            _isAttackStarted = true;
     
            _navMeshAgent.destination = gameObject.transform.position;
            
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(_secondsPauseBeforeAttack);

        if (_shouldAttack || _shouldAlwaysAttack)
        {
            var gameObjectFireball = Instantiate(_prefabFireball);
            
            var projectileController = gameObjectFireball.GetComponent<ProjectileController>();
            
            var fireballStartLocation = transform.position;
            var fireballTargetLocation = _playerRigidbody.transform.position;

            projectileController.LaunchTowards(fireballStartLocation, fireballTargetLocation);
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
