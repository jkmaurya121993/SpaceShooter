﻿using UnityEngine;
using UnityEngine.UI;

public enum GAMESTATE
{
    UI = 0,
    GAMEPLAY = 1
}

/// <summary>
/// Scripts handle the game state .
/// </summary>

public sealed class GameManager : MonoBehaviour
{
    #region PUBLIC FIELDS

    public const string  highestScoreKey = "HighestScoreKey";

    #endregion

    #region PRIVATE FIELDS

    static GameManager instance;

    [SerializeField]
    private SoundManager soundManager;

    GAMESTATE currentGameState;

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this);

    }

    #endregion

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {           
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }       
       
    }

    #region PUBLIC METHODS

    public GAMESTATE GetGameState()
    {
        return currentGameState;
    }

    // Set the game current states
    public void SetGameState(GAMESTATE state)
    {
        currentGameState = state;
    }

    // Plays common audios
    public void PlayAudio( AUDIOTYPE type, bool isLoop = false, float volume = 1)
    {
        soundManager.PlayAudio(type, isLoop, volume);
    }
    #endregion
}
