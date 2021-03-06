﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Serializable classes
[System.Serializable]
public class EnemyWaves 
{
    [Tooltip("time for wave generation from the moment the game started")]
    public float timeToStart;

    [Tooltip("Enemy wave's prefab")]
    public GameObject wave;
}

#endregion

public class LevelController : MonoBehaviour {

    #region PUBLIC FEILDS
    [SerializeField] UIManager uIManager;

    public EnemyWaves[] enemyWaves; 
    public GameObject powerUp;
    public float timeForNewPowerup;

    #endregion

    #region PRIVATE FILEDS

    int delayForEnemy = 6;

    int levels=1;

    int maxPowerUPs = 2;

    int powerCount = 0;

    Camera mainCamera;

    #endregion
   
    private void Start()
    {
        mainCamera = Camera.main;
        //for each element in 'enemyWaves' array creating coroutine which generates the wave

        InitialiseTheWave(delayForEnemy);
    }

    private void Shuffle(EnemyWaves[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            EnemyWaves value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
    }

    void InitialiseTheWave(float delay)
    {
        for (int i = 0; i < enemyWaves.Length; i++)
        {
            enemyWaves[i].timeToStart = i * delay;

            StartCoroutine(CreateEnemyWave(enemyWaves[i].timeToStart, enemyWaves[i].wave,i));
        }

        if (powerCount < maxPowerUPs)
        {
            powerCount++;
            StartCoroutine(PowerupBonusCreation());
        }
    }

    /// <summary>
    /// Create a new wave after a delay
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="Wave"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    IEnumerator CreateEnemyWave(float delay, GameObject Wave,int index) 
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);

        if (Player.instance != null)
        {
            Instantiate(Wave);

            if(index == enemyWaves.Length-1)
            {
                levels++;

                uIManager.LevelUp();
              
                yield return new WaitForSeconds(2.5f);

                Shuffle(enemyWaves);

                delayForEnemy = delayForEnemy > 1 ? delayForEnemy : 4;

                InitialiseTheWave(--delayForEnemy);

            }

        }
    }

    /// <summary>
    /// This coroutine will create PowerUp after Levelup
    /// </summary>
    /// <returns></returns>
    IEnumerator PowerupBonusCreation() 
    {
        yield return new WaitForSeconds(timeForNewPowerup);
        try
        {          
            Instantiate(powerUp, new Vector2(
                   Random.Range(PlayerMovement.instance.borders.minX, PlayerMovement.instance.borders.maxX),
                    mainCamera.ViewportToWorldPoint(Vector2.up).y + powerUp.GetComponent<Renderer>().bounds.size.y / 2),
                Quaternion.identity);
        }
        catch(System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }
       
}
