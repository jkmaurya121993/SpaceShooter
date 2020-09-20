using UnityEngine;

/// <summary>
/// Defines the damage and defines whether the projectile belongs to the ‘Enemy’ or to the ‘Player’, whether the projectile is destroyed in the collision, or not and amount of damage.
/// </summary>

public class TriggerHandler : MonoBehaviour {

    [Tooltip("Damage which a Weapon deals to another object. Integer")]
    public int damage;

    public bool enemyBullet;

    [Tooltip("Whether the Weapon will destroyed in the collision, or not")]
    public bool destroyedByCollision;

    /// <summary>
    /// when a projectile collides with another object
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (enemyBullet && collision.tag == "Player") //if anoter object is 'player' or 'enemy sending the command of receiving the damage
        {
            Player.instance.GetDamage(damage); 
            if (destroyedByCollision)
                Destroy(gameObject);
        }
        else if (!enemyBullet && collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyPlane>().GetDamage(damage);

            Player.instance.SetScore(damage);

            if (destroyedByCollision)
            {
                gameObject.SetActive(false);           
            }
               
        }
    }
}


