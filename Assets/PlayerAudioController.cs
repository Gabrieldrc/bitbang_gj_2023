using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [Header("Audio CLips")]
    [SerializeField] private AudioClip _kickSE;

    public void PlayKickSE()
    {
        _audioSource.PlayOneShot(_kickSE);
    }
}