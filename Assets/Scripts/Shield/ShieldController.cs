using UnityEngine;

public class ShieldController : MonoBehaviour
{
    // Serialize
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _shieldMaterial;

    // Private 
    private MeshRenderer _meshRenderer;
    private bool _isActiveShield;

    private void OnEnable()
    {
        MoveTowardsPoint.OnCreated += FindPlayerObject;
    }

    private void OnDisable()
    {
        MoveTowardsPoint.OnCreated -= FindPlayerObject;
    }

    public void ActivateShield()
    {
        _meshRenderer.material = _shieldMaterial;
        _isActiveShield = true;
    }

    public void DeactivateShield()
    {
        _meshRenderer.material = _defaultMaterial;
        _isActiveShield = false;
    }

    private void FindPlayerObject()
    {
        _meshRenderer = FindObjectOfType<MoveTowardsPoint>().GetComponent<MeshRenderer>();
    }

    public bool ReturnIsActiveShield()
    {
        return _isActiveShield;
    }
}
