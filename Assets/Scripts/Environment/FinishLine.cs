using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] ParticleSystem [] particles;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
        }
    }

    private void PlayParticles()
    {
        foreach (ParticleSystem particle in particles)
        {
            particle.Play();
        }
    }
}