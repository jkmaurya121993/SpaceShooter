using UnityEngine;

public class PowerUp : MonoBehaviour {

    /// <summary>
    /// when colliding with another object, if another objct is 'Player', sending command to the 'Player'
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "Player") 
        {
            if (PlayerFiring.instance.weaponPower < PlayerFiring.instance.maxweaponPower)
            {
                PlayerFiring.instance.weaponPower++;
            }
            Destroy(gameObject);
        }
    }
}
