using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MoveTowardsPoint : MonoBehaviour
{
    // Public
    public static event Action OnCreated;

    // Serialize
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private LineRenderer _pathLine;
    [SerializeField] private float _minDistanceToTarget = 1f;
    [SerializeField] private float _pathUpdateSpeed = 0.25f;
    [SerializeField] private float _pathHeight = 0.25f;
    [SerializeField] private float _delayBeforeMove = 2f;

    // Private
    private float _distanceToTarget;
    private bool _isMoving;
    private Coroutine _drawPathCoroutine;
    private Transform _targetTransform;

    private void Start()
    {
        _targetTransform = FindObjectOfType<Finish>().transform;
        Invoke(nameof(StartMove), _delayBeforeMove);
        _drawPathCoroutine = StartCoroutine(DrawPathToTarget());
        OnCreated?.Invoke();
    }

    private void Update()
    {
        if (_isMoving)
        {
            _distanceToTarget = Vector3.Distance(_targetTransform.position, transform.position);

            if (_distanceToTarget > _minDistanceToTarget)
            {
                _navMeshAgent.isStopped = false;
                _navMeshAgent.SetDestination(_targetTransform.position);
            }
            else
            {
                _navMeshAgent.isStopped = true;
            }
        }
    }

    private void StartMove()
    {
        _isMoving = true;
    }

    private IEnumerator DrawPathToTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(_pathUpdateSpeed);
        NavMeshPath path = new NavMeshPath();

        while (_targetTransform != null)
        {
            if (NavMesh.CalculatePath(transform.position, _targetTransform.position, NavMesh.AllAreas, path))
            {
                _pathLine.positionCount = path.corners.Length;

                for (int i = 0; i < path.corners.Length; i++)
                {
                    _pathLine.SetPosition(i, path.corners[i] + Vector3.up * _pathHeight);
                }
            }
            else
            {
                Debug.LogError($"Unable to calculate a path on the NavMesh between {transform.position} and {_targetTransform.position}!");
            }

            yield return wait;
        }
    }
}
