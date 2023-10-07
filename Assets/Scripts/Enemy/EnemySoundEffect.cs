using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemySoundEffect : MonoBehaviour
{
	private AudioSource _aSource;
	[SerializeField] private AudioClip _effect;
	[SerializeField] private FlyingEnemy _enemyRef;

}
