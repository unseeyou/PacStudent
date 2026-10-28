using UnityEngine;

public enum GhostState
{
    Normal = 0,
    Scared = 1
}

public class PacStudentSoundManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource moveSoundSource;
    public AudioClip moveSoundClip;
    public AudioClip eatSoundClip;
    public AudioClip levelStartClip;
    public AudioClip backgroundClipNormal;
    public AudioClip backgroundClipScared;
    public AudioClip cherrySoundClip;
    public AudioClip WallBumpAudioClip;
    
    private GhostState ghostState = GhostState.Normal;
    private float scareStartTime = -1f;
    private float start;

    private void Start()
    {
        moveSoundSource.PlayOneShot(levelStartClip);
        moveSoundSource.loop = true;
        moveSoundSource.clip = backgroundClipNormal;
        moveSoundSource.Play();
    }

    public void setGhostState(GhostState state)
    {
        if (state == ghostState)
        {
            return;
        }

        if (state == GhostState.Normal)
        {
            moveSoundSource.clip = backgroundClipNormal;
        }
        else if (state == GhostState.Scared)
        {
            moveSoundSource.clip = backgroundClipScared;
            scareStartTime = Time.time;
        }
        ghostState = state;
    }

    public void PlayStepSound()
    {
        moveSoundSource.PlayOneShot(moveSoundClip);
    }

    public void PlayEatSound()
    {
        moveSoundSource.PlayOneShot(eatSoundClip);
    }

    public void PlayCherrySound()
    {
        moveSoundSource.PlayOneShot(cherrySoundClip);
    }

    public void PlayWallBumpSound()
    {
        moveSoundSource.PlayOneShot(WallBumpAudioClip);
    }

    private void Update()
    {
        if (Time.time - scareStartTime > 10f)
        {
            setGhostState(GhostState.Normal);
        }
    }
}
