using System;
using System.Collections;
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
    private NavMeshAgent _navMeshAgent;
    
    [SerializeField] private Collider2D _attackTrigger;

    private bool _isAttackStarted;

    private bool _isPlayerInTheAttackRange;

    private float _health;

    private bool _isPlayerInTheDirectLineOfSight;
    
    private void Awake()
    {
        _health = _maxHealth;
        
        _gameObjectPlayer = GameObject.Find("Player");
        _playerRigidbody = _gameObjectPlayer.GetComponent<Rigidbody2D>();
        _gameObjectPlayer.GetComponent<PlayerMoveController>();
        
        _gameObjectPlayerBody = GameObject.Find("Player Body");
        _playerBodyCollider = _gameObjectPlayerBody.GetComponent<Collider2D>();
        
        GetComponent<Rigidbody2D>();
        
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        _isPlayerInTheDirectLineOfSight = isPlayerInTheDirectLineOfSIghtVisible();
    }

    private bool isPlayerInTheDirectLineOfSIghtVisible()
    {
        if (_playerRigidbody != null)
        {
            var playerDirection = (_playerRigidbody.transform.position - transform.position).normalized;
            var raycasts = Physics2D.RaycastAll(transform.position, playerDirection);

            foreach (var raycast in raycasts)
            {
                if (raycast.transform.GetComponent<Block>() != null)
                {
                    return false;
                } 
                
                if (raycast.transform.GetComponent<PlayerDamageController>() != null)
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    private void FixedUpdate()
    {
        if (_shouldMove)
        {
            if (!_isAttackStarted && (!_isPlayerInTheDirectLineOfSight || !_isPlayerInTheAttackRange) && _gameObjectPlayer != null)
            {
                _navMeshAgent.destination = _gameObjectPlayer.transform.position;
            }
            else
            {
                attackIfRequired();
            }  
        }
        else
        {
            attackIfRequired();
        }
    }

    private void attackIfRequired()
    {
        if (!_isAttackStarted && (_isPlayerInTheAttackRange || _shouldAlwaysAttack) && _isPlayerInTheDirectLineOfSight)
        {
            _isAttackStarted = true;

            if (_shouldMove)
            {
                _navMeshAgent.destination = gameObject.transform.position;
            }

            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(_secondsPauseBeforeAttack);

        if (_isPlayerInTheDirectLineOfSight && (_isPlayerInTheAttackRange || _shouldAlwaysAttack))
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
            _isPlayerInTheAttackRange = true;
        }
    }

    public void OnAttackTriggerExit(Collider2D other)
    {
        if (_playerBodyCollider == other)
        {
            _isPlayerInTheAttackRange = false;
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
