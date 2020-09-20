using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// This script generates an enemy wave. It defines how many enemies will be emerging, their speed and emerging interval. 
/// It also defines their shooting mode. It defines their moving path.
/// </summary>
[System.Serializable]
public class WeaponFire
{
    [Range(0,100)]
    [Tooltip("probability with which the ship of this wave will make a shot")]
    public int fireChance;

    [Tooltip("min and max time from the beginning of the path when the enemy can make a shot")]
    public float minTime, maxTime;
}

public class Path : MonoBehaviour {

    #region PUBLIC FIELDS

    [Tooltip("Enemy number in wave")]
    public int count;

    [Tooltip("Path passage speed")]
    public float speed;

    [Tooltip("Time delay in emerging of the enemies in the wave")]
    public float delay;

    [Tooltip("Enemy will fallow this path")]
    public Transform[] pathPoints;
    public WeaponFire shooting;

    [SerializeField] ScriptableObjectData objectData;
    private GameObject enemy;
    #endregion

    private void Start()
    {
        int enemyIndex = UnityEngine.Random.Range(0,5);
        enemy = objectData.enemyPlane[enemyIndex];
        StartCoroutine(CreateEnemyWave()); 
    }
    /// <summary>
    /// Enemy Creation
    /// </summary>
    /// <returns></returns>
    IEnumerator CreateEnemyWave() 
    {
        for (int i = 0; i < count; i++) 
        {
            GameObject enemyObject;
            enemyObject = Instantiate(enemy, enemy.transform.position, Quaternion.identity);
            WayPoints waypoints = enemyObject.GetComponent<WayPoints>(); 
            waypoints.pathPoints = pathPoints;         
            waypoints.speed = speed;        
            waypoints.SetPath(); 
            EnemyPlane enemyClass = enemyObject.GetComponent<EnemyPlane>();  
            enemyClass.shotChance = shooting.fireChance; 
            enemyClass.shotTimeMin = shooting.minTime; 
            enemyClass.shotTimeMax = shooting.maxTime;
            enemyObject.SetActive(true);      
            yield return new WaitForSeconds(delay); 
        }
       
            Destroy(gameObject); 
    }   
}
