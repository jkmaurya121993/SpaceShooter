﻿using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script defines which sprite the 'Player" uses and its health.
/// </summary>

public class Player : MonoBehaviour
{
    #region PUBLIC FIELDS

    [Tooltip("Health of Player")]
    [SerializeField] int playerHealth;
 
    [Tooltip("VFX Prefab Refernces")]
    [SerializeField] GameObject destructionFX;
    [SerializeField] GameObject hitEffect;
    [SerializeField] UIManager uIManager;

    public static Player instance;

    #endregion

    #region PRIVATE

    float maxHealth;

    Vector3 initialPosition;

    int playerScore;

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        if (instance == null) 
            instance = this;

        initialPosition = transform.position;

        maxHealth = playerHealth;
    }

    #endregion

    #region PUBLIC METHODS
    /// <summary>
    /// method for damage proceccing by 'Player'
    /// </summary>
    /// <param name="damage"></param>
    public void GetDamage(int damage)   
    {
        uIManager.SetPlayerHealth(damage, maxHealth);
        
        playerHealth -= damage;           //reducing health for damage value, if health is less than 0, Destroy player

        if (playerHealth <= 0)
        {
            Destruction();
        }
        else
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity, transform);

            transform.position = initialPosition;

        }
    }
    /// <summary>
    /// score calculation
    /// </summary>
    /// <param name="scoreValue"></param> 
    public void SetScore(int scoreValue)
    {
        playerScore += scoreValue;

        uIManager.SetScore(playerScore);
    }

    #endregion

    #region PRIVATE METHODS

    /// <summary>
    /// When player exaust all its health.
    /// </summary>
    void Destruction()
    {
        GameManager.Instance.PlayAudio(AUDIOTYPE.EXPLOSION);

        int previousScore = PlayerPrefs.GetInt(GameManager.highestScoreKey);

        if (previousScore < playerScore)
        {
            PlayerPrefs.SetInt(GameManager.highestScoreKey, playerScore);
        }

        uIManager.GameOver();

        Time.timeScale = 0;

        Instantiate(destructionFX, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
        Destroy(gameObject);
    }
    #endregion
}
















