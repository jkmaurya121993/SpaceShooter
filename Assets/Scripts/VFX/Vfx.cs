using System.Collections;
using UnityEngine;

/// <summary>
/// This script attaches to ‘VisualEffect’ objects. It destroys or deactivates them after the defined time.
/// </summary>
public class Vfx : MonoBehaviour {

    [Tooltip("the time after object will be destroyed")]
    public float destructionTime;

    private void OnEnable()
    {
        StartCoroutine(DestroyVfX()); 
    }

    /// <summary>
    /// wait for the estimated time, and destroying or deactivating the object
    /// </summary>
    /// <returns></returns>
    IEnumerator DestroyVfX() 
    {
        yield return new WaitForSeconds(destructionTime);
       // gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
