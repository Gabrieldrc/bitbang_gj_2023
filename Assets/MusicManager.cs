using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _powerMusicClip;
    [SerializeField] private float _powerMusicVolume;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.OnSuperActivated += PlayPowerMusic;
    }

    private void PlayPowerMusic(bool isActive)
    {
        if (isActive)
        {
            _audioSource.Pause();
            _audioSource.PlayOneShot(_powerMusicClip, _powerMusicVolume);
        }
        else
        {
            _audioSource.Play();
        }
    }

    private void OnDisable()
    {
        _player.OnSuperActivated -= PlayPowerMusic;
    }
}