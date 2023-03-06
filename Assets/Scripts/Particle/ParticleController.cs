using System;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    // Public
    public static event Action OnParticleEnd;

    // Serialize
    [SerializeField] private ParticleSystem _confettiParticle;
    [SerializeField] private ParticleSystem _scatteringParticle;

    private void OnEnable()
    {
        Finish.OnFinishPosition += PlayConfettiParticle;
        DeadZone.OnDeath += PlayScatteringParticle;
    }

    private void OnDisable()
    {
        Finish.OnFinishPosition -= PlayConfettiParticle;
        DeadZone.OnDeath -= PlayScatteringParticle;
    }

    private void PlayConfettiParticle(Vector3 position)
    {
        transform.position = position;

        if (!_confettiParticle.isPlaying)
        {
            _confettiParticle.Play();
        }
    }

    private void PlayScatteringParticle(Vector3 position)
    {
        transform.position = position;

        if (!_scatteringParticle.isPlaying)
        {
            _scatteringParticle.Play();
        }

        Invoke(nameof(InvokeOnParticleEnd), 2f);
    }

    private void InvokeOnParticleEnd()
    {
        OnParticleEnd?.Invoke();
    }
}
