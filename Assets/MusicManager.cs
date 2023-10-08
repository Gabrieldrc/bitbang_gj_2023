using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource _audioSource;
    private AudioClip _powerMusicClip;
    private float _powerMusicVolume;

    public void PlayPowerMusic()
    {
        _audioSource.PlayOneShot(_powerMusicClip, _powerMusicVolume);
    }
}
