using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    [SerializeField]
    private float _idleTime = 2f;

    [SerializeField]
    private List<Transform> _patrolPoints;
    [SerializeField]
    private NavMeshAgent _agent;

    [SerializeField]
    private Transform _playerTransform;
    [SerializeField]
    private float _chaseDistance = 5f;

    private ZombieState _currentState = ZombieState.Idle;
    private float _idleTimer = 0f;
    //private int _currentPatrolPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        _currentState = ZombieState.Idle;
        _idleTimer = _idleTime;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_currentState)
        {
            case ZombieState.Idle:
                Idle();
                break;
            case ZombieState.Patrol:
                Patrol();
                break;
            case ZombieState.Chase:
                Chase();
                break;
        }
    }

    void Idle()
    {
        _idleTimer -= Time.deltaTime;

        if (_idleTimer <= 0f)
        {
            var rd = Random.Range(0, _patrolPoints.Count);
            _agent.SetDestination(_patrolPoints[rd].position);

            _currentState = ZombieState.Patrol;
        }

        if (Vector3.Distance(transform.position, _playerTransform.position) <= _chaseDistance)
        {
            _currentState = ZombieState.Chase;
        }
    }

    void Patrol()
    {
        if (_agent.remainingDistance <= 0.5f)
        {
            _idleTimer = _idleTime;
            _currentState = ZombieState.Idle;
        }

        if (Vector3.Distance(transform.position, _playerTransform.position) <= _chaseDistance)
        {
            _currentState = ZombieState.Chase;
        }
    }

    void Chase()
    {
        _agent.SetDestination(_playerTransform.position);

        if (Vector3.Distance(transform.position, _playerTransform.position) > _chaseDistance)
        {
            _idleTimer = _idleTime;
            _currentState = ZombieState.Idle;
        }
    }
}

public enum ZombieState
{
    Idle,
    Patrol,
    Chase
}
