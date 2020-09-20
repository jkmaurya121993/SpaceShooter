using UnityEngine;

/// <summary>
/// This script defines 'Enemy's' health and behavior. 
/// </summary>

public class EnemyPlane : MonoBehaviour {

    #region PUBLIC FIELDS
    [Tooltip("Health points in integer")]
    public int health;
    [Tooltip("VFX prefab generating after destruction")]
    public GameObject destructionVFX;
    public GameObject hitEffect;
    /// <summary>
    /// probability of 'Enemy's' shooting during the path
    /// </summary>
    [HideInInspector] public int shotChance;

    /// <summary>
    /// max and min time for shooting from the beginning of the path
    /// </summary>
    [HideInInspector] public float shotTimeMin, shotTimeMax;
    #endregion
    
    private GameObject enemyWeapon;
    [SerializeField] private ScriptableObjectData weaponData;
    private void Start()
    {
        enemyWeapon = weaponData.enemyWeapon[Random.Range(0,4)];
        Invoke("ActivateShooting", Random.Range(shotTimeMin, shotTimeMax));
       
    }

    /// <summary>
    /// making a shot
    /// </summary> 
    void ActivateShooting() 
    {
        if (Random.value < (float)shotChance / 100)                             //if random value less than shot probability, make a shot
        {

            Instantiate(enemyWeapon, gameObject.transform.position, Quaternion.identity);
        }
    }

    /// <summary>
    /// method of getting damage for the 'Enemy'
    /// </summary>
    /// <param name="damage"></param>
    public void GetDamage(int damage) 
    {
        health -= damage;           //reducing health for damage value, if health is less than 0, destroy object
        if (health <= 0)
            DestroyEnemyObject();
        else
        {        
           Instantiate(hitEffect, transform.position, Quaternion.identity, transform);
        }
    }

    /// <summary>
    /// if 'Enemy' collides 'Player', 'Player' gets the damage equal to projectile's damage value
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (enemyWeapon.GetComponent<TriggerHandler>() != null)
                Player.instance.GetDamage(enemyWeapon.GetComponent<TriggerHandler>().damage);
            else
                Player.instance.GetDamage(1);
        }
    }
    /// <summary>
    /// method of destroying the 'Enemy'
    /// </summary>
    void DestroyEnemyObject()                           
    {
        GameManager.Instance.PlayAudio(AUDIOTYPE.EXPLOSION);
        Instantiate(destructionVFX, transform.position, Quaternion.identity);      
        Destroy(gameObject);
    }
}
