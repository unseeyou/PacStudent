using UnityEngine;

public class PacStudentSoundManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource moveSoundSource;
    public AudioClip moveSoundClip;
    public AudioClip eatSoundClip;
    
    public void PlayStepSound()
    {
        moveSoundSource.PlayOneShot(moveSoundClip);
    }

    public void PlayEatSound()
    {
        moveSoundSource.PlayOneShot(eatSoundClip);
    }
}
