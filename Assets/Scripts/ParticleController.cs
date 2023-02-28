using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    // Serialize
    [SerializeField] private ParticleSystem _confettiParticle;
    [SerializeField] private ParticleSystem _scatteringParticle;

    public void PlayConfettiParticle()
    {
        if (!_confettiParticle.isPlaying)
        {
            _confettiParticle.Play();
        }
    }

    public void PlayScatteringParticle()
    {
        if (!_scatteringParticle.isPlaying)
        {
            _scatteringParticle.Play();
        }
    }
}
