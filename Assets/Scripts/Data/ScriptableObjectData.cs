using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/ScriptableObjectData", order = 1)]
public class ScriptableObjectData : ScriptableObject
{
    public List<GameObject> enemyPlane = new List<GameObject>(); // It contain enemyShip Prefab
    public List<GameObject> enemyWeapon = new List<GameObject>();// It contain enemy weapon prefab
}
