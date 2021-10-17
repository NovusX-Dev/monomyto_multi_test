using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    enum States { Idle, Patrol, Chase, Shoot};

    [Header("Movement")]
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _idleWaitTime = 3f;

    [Header("Attack")]
    [SerializeField] float _gunPowerMultiplier = 1f;
    [SerializeField] float _fireRateMultiplier = 2f;
    [SerializeField] float _chaseDistance = 5f;
    [SerializeField] Weapon _equipedWeapon;

    [Header("Other")]
    [SerializeField] LayerMask _playersMask;


    private PlayerAttacker _player;
    private Vector3 _targetPosition;
    private float _aggroRange;
    [SerializeField]private States _states;
    private bool _playerVisible;
    private bool _isAlert;
    private bool _isChasing;
    private bool _isPatroling;
    private bool _isShooting;
    private RaycastHit2D _visiblePlayers;
    private float _currentGunPower;
    private float _currentFireRate;
    private float _nextFire = -1;


    void Start()
    {
        _states = States.Idle;
        _aggroRange = _chaseDistance / 1.25f;

        _currentFireRate = _equipedWeapon.FireRate * _fireRateMultiplier;
        _currentGunPower = _equipedWeapon.SetBulletPower(_equipedWeapon.GetBulletPower() * _gunPowerMultiplier);
    }

    void Update()
    {
        var _visiblePlayers = Physics2D.CircleCast(transform.position, _chaseDistance, Vector2.right, _chaseDistance, _playersMask);
        _playerVisible = _visiblePlayers.point != Vector2.zero ? true : false;

        //_player = _playerVisible ? _visiblePlayers.collider.GetComponent<PlayerAttacker>() : null;
        if(_playerVisible)
        {
            if(_visiblePlayers.collider.GetComponent<PlayerAttacker>() != null)
                _player = _visiblePlayers.collider.GetComponent<PlayerAttacker>();
        }
        else
        {
            _player = null;
        }

        switch (_states)
        {
            case States.Idle:
                IdleState();

                break;

            case States.Patrol:
                PatrolState();
                break;

            case States.Chase:
                ChaseState();
                break;

            case States.Shoot:
                ShootState();

                break;
        }
        if (_playerVisible)
        {
            RotateEnemy((Vector3)_player.transform.position);
        }
        else
        {
            RotateEnemy((Vector3)_targetPosition);
        }
    }

    private void IdleState()
    {
        //_player = null;
        _targetPosition = Vector3.zero;
        _isAlert = true;
        _isChasing = false;
        _isPatroling = false;
        _isShooting = false;

        if (_playerVisible)
        {
            _states = States.Chase;
            //_player = _visiblePlayers.collider.GetComponent<PlayerAttacker>();
        }

        if (_isAlert) StartCoroutine(PatrolRoutine());
    }

    private void PatrolState()
    {
        _isAlert = false;
        _isPatroling = true;
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);

        if (_playerVisible)
        {
            _states = States.Chase;
            //_player = _visiblePlayers.collider.GetComponent<PlayerAttacker>();
        }

        if (Vector3.Distance(transform.position, _targetPosition) < 0.25f)
        {
            if (_isPatroling)
                StartCoroutine(GoingIdleRoutine());
        }
    }

    private void ChaseState()
    {
        _isAlert = false;
        _isChasing = true;
        _isPatroling = false;
        _isShooting = false;

        //_targetPosition = _player.transform.position;

        if (Vector3.Distance(transform.position, _player.transform.position) > _aggroRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
        }
        else if (Vector3.Distance(transform.position, _player.transform.position) < _aggroRange && _playerVisible)
        {
            _states = States.Shoot;
        }

        if (Vector3.Distance(transform.position, _player.transform.position) > (_chaseDistance + 0.5f) || _player == null)
        {
            _states = States.Idle;
        }
    }

    private void ShootState()
    {
        _isShooting = true;
        _isChasing = false;

        //_targetPosition = _player.transform.position;

        if(Time.time > _nextFire)
        {
            _equipedWeapon.FireWeapon();
            _nextFire = Time.time + _currentFireRate;
        }

        if (Vector3.Distance(transform.position, _player.transform.position) > _aggroRange)
        {
            _states = States.Chase;
        }
    }

    IEnumerator GoingIdleRoutine()
    {
        yield return new WaitForSeconds(_idleWaitTime);
        _states = States.Idle;
    }

    IEnumerator PatrolRoutine()
    {
        if (!_isPatroling && _isChasing || _isShooting) yield break;
        yield return new WaitForSeconds(_idleWaitTime);
        if(!_isPatroling)
            _targetPosition = new Vector3(transform.position.x + Random.Range(-4f, 4f), transform.position.y + Random.Range(-4f, 4f), 0f);
        _states = States.Patrol;
    }

    private void RotateEnemy(Vector3 target)
    {
        var lookDir = target - transform.position;
        var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _chaseDistance);
        Gizmos.color = Color.red;
    }
}
