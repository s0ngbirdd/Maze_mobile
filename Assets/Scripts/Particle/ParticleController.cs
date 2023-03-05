using UnityEngine;

public class ParticleController : MonoBehaviour
{
    // Serialize
    [SerializeField] private ParticleSystem _confettiParticle;
    [SerializeField] private ParticleSystem _scatteringParticle;

    // Private
    private MazeGenerator _mazeGenerator;

    private void Start()
    {
        _mazeGenerator = FindObjectOfType<MazeGenerator>();
    }

    public void PlayConfettiParticle(Vector3 position)
    {
        transform.position = position;

        if (!_confettiParticle.isPlaying)
        {
            _confettiParticle.Play();
        }
    }

    public void PlayScatteringParticle(Vector3 position)
    {
        transform.position = position;

        if (!_scatteringParticle.isPlaying)
        {
            _scatteringParticle.Play();
        }

        Invoke(nameof(InstantiatePlayer), 2f);
    }

    private void InstantiatePlayer()
    {
        if (!FindObjectOfType<MoveTowardsPoint>())
        {
            _mazeGenerator.InstantiatePlayer();
        }
    }
}
