using UnityEngine;

/// <summary>
/// This script defines the size of the ‘Boundary’ depending on Viewport. When objects go beyond the ‘Boundary’, they are destroyed or deactivated.
/// </summary>

public class Boundary : MonoBehaviour {

    BoxCollider2D boundareCollider;
    /// <summary>
    ///  receiving collider's component and changing boundary borders
    /// </summary>

    private void Start()
    {
        boundareCollider = GetComponent<BoxCollider2D>();
        ResizeCollider();
    }
    /// <summary>
    /// changing the collider's size up to Viewport's size multiply 1.5
    /// </summary>
    void ResizeCollider() 
    {        
        Vector2 viewportSize = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) * 2;
        viewportSize.x *= 1.5f;
        viewportSize.y *= 1.5f;
        boundareCollider.size = viewportSize;
    }

    /// <summary>
    /// when another object leaves collider
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision) 
    {        
        if (collision.tag == "Bullet")
        {                    
            collision.gameObject.SetActive(false);          
        }
        else if (collision.tag == "Bonus")            
            Destroy(collision.gameObject);
    }

}
