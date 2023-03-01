using UnityEngine;

public class DeadZone : MonoBehaviour
{
    // Private
    private ShieldController _shieldController;
    private ParticleController _particleController;

    private void Start()
    {
        _shieldController = FindObjectOfType<ShieldController>();
        _particleController = FindObjectOfType<ParticleController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_shieldController.ReturnIsActiveShield())
        {
            Destroy(other.gameObject);
            _particleController.PlayScatteringParticle(other.gameObject.transform.position);
        }
    }
}
