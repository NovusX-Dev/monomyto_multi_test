using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    enum States { Idle, Patrol, Chase, Shoot};

    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _chaseDistance = 5f;
    [SerializeField] LayerMask _playersMask;

    private Vector2 _targetPosition;
    private float _aggroRange;
    [SerializeField]private States _states;
    private bool _playerVisible;
    private bool _isChasing;
    private bool _isMoving;
    private bool _isShooting;
    private RaycastHit2D _visiblePlayers;


    void Start()
    {
        _states = States.Idle;
        _aggroRange = _chaseDistance / 1.25f;
    }

    void Update()
    {
        var _visiblePlayers = Physics2D.CircleCast(transform.position, _chaseDistance, Vector2.right, _chaseDistance, _playersMask);

        _playerVisible = _visiblePlayers.point != Vector2.zero ? true : false;
        if (_playerVisible)
        {
            _states = States.Chase;
        }
        else _states = States.Idle;


        switch (_states)
        {
            case States.Idle:

                break;

            case States.Patrol:

                break;

            case States.Chase:
                _targetPosition = _visiblePlayers.point;
                if (Vector2.Distance(transform.position, _targetPosition) > _aggroRange)
                {
                    transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);
                }
                else
                {
                    _states = States.Shoot;
                }
                break;

            case States.Shoot:

                break;
        }

        RotateEnemy((Vector3)_targetPosition);
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
