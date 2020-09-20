using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// This script moves the ‘Enemy’ along the defined path.
/// </summary>
public class WayPoints : MonoBehaviour {
        
    [HideInInspector] public Transform [] pathPoints; //path points which passes the 'Enemy' 
    [HideInInspector] public float speed;
    float currentPathPercent;               //current percentage of completing the path
    Vector3[] pathPositions;               
    [HideInInspector] public bool isObjectMoving;   //whether 'Enemy' moves or not

    /// <summary>
    /// setting path parameters for the 'Enemy' and sending the 'Enemy' to the path starting point
    /// </summary>
    
    public void SetPath() 
    {
        currentPathPercent = 0;
        pathPositions = new Vector3[pathPoints.Length];       //transform path points to vector3
        for (int i = 0; i < pathPositions.Length; i++)
        {
            pathPositions[i] = pathPoints[i].position;
        }
        transform.position = NewPositionByPath(pathPositions, 0); //sending the enemy to the path starting point
        isObjectMoving = true;
    }

    private void Update()
    {
        if (isObjectMoving)
        {
            currentPathPercent += speed / 100 * Time.deltaTime;     //every update calculating current path percentage according to the defined speed

            transform.position = NewPositionByPath(pathPositions, currentPathPercent); //moving the 'Enemy' to the path position, calculated in method NewPositionByPath

            transform.right = Interpolate(CreatePathPoints(pathPositions), currentPathPercent + 0.01f) - transform.position;
            
            transform.Rotate(Vector3.forward * -90);   
           
            if (currentPathPercent > 1)                    //when the path is complete
            {                           
               Destroy(gameObject);                           
            }
        }
    }

    Vector3 NewPositionByPath(Vector3 [] pathPos, float percent) 
    {
        return Interpolate(CreatePathPoints(pathPos), currentPathPercent);
    }

    Vector3 Interpolate(Vector3[] path, float t) 
    {
        int numSections = path.Length - 3;
        int currPt = Mathf.Min(Mathf.FloorToInt(t * numSections), numSections - 1);
        float u = t * numSections - currPt;
        Vector3 a = path[currPt];
        Vector3 b = path[currPt + 1];
        Vector3 c = path[currPt + 2];
        Vector3 d = path[currPt + 3];
        return 0.5f * ((-a + 3f * b - 3f * c + d) * (u * u * u) + (2f * a - 5f * b + 4f * c - d) * (u * u) + (-a + c) * u + 2f * b);
    }

    Vector3[] CreatePathPoints(Vector3[] path) 
    {
        Vector3[] pathPositions;
        Vector3[] newPathPosition;
        int dist = 2;
        pathPositions = path;
        newPathPosition = new Vector3[pathPositions.Length + dist];
        Array.Copy(pathPositions, 0, newPathPosition, 1, pathPositions.Length);
        newPathPosition[0] = newPathPosition[1] + (newPathPosition[1] - newPathPosition[2]);
        newPathPosition[newPathPosition.Length - 1] = newPathPosition[newPathPosition.Length - 2] + (newPathPosition[newPathPosition.Length - 2] - newPathPosition[newPathPosition.Length - 3]);
        return (newPathPosition);
    }
}
