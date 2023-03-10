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
    [SerializeField] private GameObject[] _wallObjects;
    [SerializeField] private MeshRenderer _floorMeshRenderer;
    [SerializeField] private GameObject _deadZoneObject;
    [SerializeField] private float _chanceToActivateDeadZone = 10f;

    // Private
    private bool _removeDeadZone;

    private void Start()
    {
        if (Random.Range(0, 100) < _chanceToActivateDeadZone && !_removeDeadZone)
        {
            _deadZoneObject.SetActive(true);
        }
    }

    public void RemoveWall(int wallToRemove)
    {
        _wallObjects[wallToRemove].gameObject.SetActive(false);
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
                _floorMeshRenderer.material.color = Color.white;
                break;
            case NodeState.CURRENT:
                _floorMeshRenderer.material.color = Color.yellow;
                break;
            case NodeState.COMPLETED:
                _floorMeshRenderer.material.color = Color.blue;
                break;
        }
    }
}
