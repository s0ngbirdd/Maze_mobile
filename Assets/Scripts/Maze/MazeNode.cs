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

    // Private
    private bool _removeDeadZone;

    private void Start()
    {
        if (Random.Range(0, 100) <= _chanceToActivateDeadZone && !_removeDeadZone)
        {
            _deadZone.SetActive(true);
        }
    }

    public void RemoveWall(int wallToRemove)
    {
        _walls[wallToRemove].gameObject.SetActive(false);
    }

    public void RemoveDeadZone()
    {
        _removeDeadZone = true;
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
}
