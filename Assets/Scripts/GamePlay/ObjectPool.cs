using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    private void Awake()
    {
        SharedInstance = this;
    }
    private void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject newitem;

        for(int i=0;i<amountToPool;i++)
        {
            newitem = Instantiate(objectToPool,transform);
            newitem.SetActive(false);
            pooledObjects.Add(newitem);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i=0;i<amountToPool;i++)
        {
            if(pooledObjects[i]!=null)
            if(!pooledObjects[i].activeInHierarchy)
            { 
                    GameObject item = pooledObjects[i]; ;
                    return item;
            }
        }

        return null;
    }

}
