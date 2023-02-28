using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    // Serialize
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _shieldMaterial;

    // Private 
    private MeshRenderer _meshRenderer;
    private bool _isActiveShield;

    private void Start()
    {
        Invoke(nameof(FindObject), 0.1f);
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

    private void FindObject()
    {
        _meshRenderer = FindObjectOfType<MoveTowardsPoint>().GetComponent<MeshRenderer>();
    }

    public bool ReturnIsActiveShield()
    {
        return _isActiveShield;
    }
}
