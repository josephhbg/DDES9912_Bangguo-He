using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource audioSource;
    public AudioClip rotClip,selClip,runClip;

    private void Awake()
    {
        instance = this;
    }

    public void PlayRotate()
    {
        audioSource.PlayOneShot(rotClip);
    }

    public void PlaySele()
    {
        audioSource.PlayOneShot(selClip);
    }

    public void PlayRun()
    {
        audioSource.PlayOneShot(runClip);
    }
}
