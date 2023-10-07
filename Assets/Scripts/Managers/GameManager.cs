using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    [SerializeField] private Transform _playerPosRef;

    public Transform PlayerPosRef
    {
        get => _playerPosRef;
        set => _playerPosRef = value;
    }

    public int totalScore = 0;

    public void AddPoints(int points)
    {
        totalScore += points;
        //TODO make points sounds
    }
}