using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem stepParticles;
    
    public void PlayParticleEffect()
    {
        if (stepParticles != null)
        {
            stepParticles.Play();
        }
    }
}
