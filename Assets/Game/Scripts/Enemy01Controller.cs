using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy01Controller : MonoBehaviour
{
    [SerializeField] private float _secondsPauseBeforeAttack;
    [SerializeField] private float _secondsPauseAfterAttack;
    [SerializeField] private float _secondsPlayerStun;
    [SerializeField] private float _pushForce;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _damage;
    
    private GameObject _gameObjectPlayer;
    private Rigidbody2D _playerRigidbody;
    private GameObject _gameObjectPlayerBody;
    private Collider2D _playerBodyCollider;
    private PlayerMoveController _playerMoveController;
    private PlayerDamageController _playerDamageController;
    private NavMeshAgent _navMeshAgent;
    
    [SerializeField] private Collider2D _attackTrigger;

    [SerializeField] private Animator _animator;
    
    private bool _isAttackStarted;

    private bool _shouldAttack;

    private float _health;
    
    private int _animationHashIsWalking;
    private int _animationHashAttack;
    
    private void Awake()
    {
        _health = _maxHealth;
        
        _gameObjectPlayer = GameObject.Find("Player");
        _playerRigidbody = _gameObjectPlayer.GetComponent<Rigidbody2D>();
        _playerMoveController = _gameObjectPlayer.GetComponent<PlayerMoveController>();
        _playerDamageController = _gameObjectPlayer.GetComponent<PlayerDamageController>();
        
        _gameObjectPlayerBody = GameObject.Find("Player Body");
        _playerBodyCollider = _gameObjectPlayerBody.GetComponent<Collider2D>();
        
        GetComponent<Rigidbody2D>();

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        
        _animationHashIsWalking = Animator.StringToHash("IsWalking");
        _animationHashAttack = Animator.StringToHash("Attack");
    }

    private void FixedUpdate()
    {
        if (!_isAttackStarted && !_shouldAttack && _gameObjectPlayer != null)
        {
            _navMeshAgent.destination = _gameObjectPlayer.transform.position;
            
            _animator.SetBool(_animationHashIsWalking, true);
        }
        else if (!_isAttackStarted && _shouldAttack)
        {
            _isAttackStarted = true;

            _navMeshAgent.destination = gameObject.transform.position;
            
            _animator.SetBool(_animationHashIsWalking, false);
            
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
            
            _animator.SetTrigger(_animationHashAttack);
            
            yield return new WaitForSeconds(0.4f);
            
            _playerRigidbody.drag = 10.0f;
            _playerRigidbody.AddForce(force * _pushForce, ForceMode2D.Impulse);
            
            if (_playerDamageController.Damage(_damage))
            {
                yield return new WaitForSeconds(_secondsPlayerStun);

                _playerMoveController.EnableMovement();
            }
        }
        else
        {
            _animator.SetTrigger(_animationHashAttack);
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
