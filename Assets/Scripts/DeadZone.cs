using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadZone : MonoBehaviour
{
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
            other.gameObject.GetComponent<ParticleController>().PlayScatteringParticle();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
