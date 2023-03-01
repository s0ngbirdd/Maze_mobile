using UnityEngine;

public class Finish : MonoBehaviour
{
    // Private
    private ParticleController _particleController;
    private UIController _UIController;

    private void Start()
    {
        _particleController = FindObjectOfType<ParticleController>();
        _UIController = FindObjectOfType<UIController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _particleController.PlayConfettiParticle(other.gameObject.transform.position);
        _UIController.EnableEndFade();
    }
}
