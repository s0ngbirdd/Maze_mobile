using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    AVAILABLE,
    CURRENT,
    COMPLETED
}

public class MazeNode : MonoBehaviour
{
    // Serialize
    [SerializeField] private GameObject[] _walls;
    [SerializeField] private MeshRenderer _floor;
    [SerializeField] private GameObject _deadZone;
    [SerializeField] private float _chanceToActivateDeadZone = 10f;

    private void Start()
    {
        if (Random.Range(0, 100) <= _chanceToActivateDeadZone)
        {
            _deadZone.SetActive(true);
        }
    }

    // for debug
    public void SetState(NodeState state)
    {
        switch (state)
        {
            case NodeState.AVAILABLE:
                _floor.material.color = Color.white;
                break;
            case NodeState.CURRENT:
                _floor.material.color = Color.yellow;
                break;
            case NodeState.COMPLETED:
                _floor.material.color = Color.blue;
                break;
        }
    }

    public void RemoveWall(int wallToRemove)
    {
        _walls[wallToRemove].gameObject.SetActive(false);
    }

    public void RemoveDeadZone()
    {
        _deadZone.SetActive(false);
    }
}
