using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
   public static ObjectPool SharedInstance;
   #region SerializeField
   [SerializeField] private List<GameObject> pooledObjects;
   [SerializeField] private GameObject objectToPool;
   [SerializeField] private int amountToPool;
    #endregion

    #region Unity Method
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
    #endregion

    /// <summary>
    /// Method to getting pool object
    /// </summary>
    /// <returns></returns>
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
