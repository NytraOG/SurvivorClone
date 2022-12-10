using UnityEngine;

public class BattleTrack : MonoBehaviour
{
    public AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(transform.gameObject);
    }

    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;

        audioSource.Play();
    }

    public void StopMusic() => audioSource.Stop();
}