using System;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    // Public
    public static event Action<Vector3> OnDeath;

    // Private
    private ShieldController _shieldController;

    private void Start()
    {
        _shieldController = FindObjectOfType<ShieldController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_shieldController.ReturnIsActiveShield())
        {
            Destroy(other.gameObject);
            OnDeath?.Invoke(other.gameObject.transform.position);
        }
    }
}
