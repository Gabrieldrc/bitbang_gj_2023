using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [Header("Audio CLips")]
    [SerializeField] private AudioClip _kickSE;
    [SerializeField] private float _kickVolume = .5f;
    [SerializeField] private AudioClip _jumpSE;
    [SerializeField] private float _jumpVolume = .5f;

    public void PlayKickSE()
    {
        _audioSource.PlayOneShot(_kickSE, _kickVolume);
    }

    public void PlayJumpSE()
    {
        _audioSource.PlayOneShot(_jumpSE, _jumpVolume);
    }
}