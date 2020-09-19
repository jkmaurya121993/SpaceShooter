using UnityEngine;

//guns objects in 'Player's' hierarchy
[System.Serializable]
public class Guns
{
    public GameObject rightGun, leftGun, centralGun;
    [HideInInspector] public ParticleSystem leftGunVFX, rightGunVFX, centralGunVFX; 
}

public class PlayerShooting : MonoBehaviour {

    #region PUBLIC FIELDS

    [Tooltip("shooting frequency. the higher the more frequent")]
    public float fireRate;

    [Tooltip("projectile prefab")]
    public GameObject projectileObject;

    //time for a new shot
    [HideInInspector] public float nextFire;

    [Tooltip("current weapon power")]
    [Range(1, 4)]       //change it if you wish
    public int weaponPower = 1; 

    public Guns guns;
    bool shootingIsActive = true; 
    [HideInInspector] public int maxweaponPower = 4; 
    public static PlayerShooting instance;

    #endregion

    #region PRIVATE FIELDS
    ObjectPool objectPool;
    GameManager gameManager;

    #endregion

    #region UNITY METHODS

    private void Awake()
    {
        if (instance == null)
            instance = this;

        gameManager = GameManager.Instance;
    }
    private void Start()
    {
        objectPool = ObjectPool.SharedInstance;     
        guns.leftGunVFX = guns.leftGun.GetComponent<ParticleSystem>();
        guns.rightGunVFX = guns.rightGun.GetComponent<ParticleSystem>();
        guns.centralGunVFX = guns.centralGun.GetComponent<ParticleSystem>();
        gameManager.PlayAudio(AUDIOTYPE.PLAYERSHOT, true);
    }

    private void Update()
    {
        if (shootingIsActive)
        {
            if (Time.time > nextFire)
            {
                CreatBulletShot();                                                         
                nextFire = Time.time + 1 / fireRate;
            }
        }
    }

    #endregion

    #region PRIVATE METHODS
    /// <summary>
    /// method for a shot
    /// According to weaponPower bullet shot will creat
    /// </summary>

    private void CreatBulletShot() 
    {
        switch (weaponPower) 
        {
            case 1:
                EnableBulletShot(objectPool.GetPooledObject(), guns.centralGun.transform.position, Vector3.zero);
                guns.centralGunVFX.Play();
                break;
            case 2:           
                EnableBulletShot(objectPool.GetPooledObject(), guns.rightGun.transform.position, Vector3.zero);
                guns.leftGunVFX.Play();
               
                EnableBulletShot(objectPool.GetPooledObject(), guns.leftGun.transform.position, Vector3.zero);
                guns.rightGunVFX.Play();
                break;
            case 3:              
                EnableBulletShot(objectPool.GetPooledObject(), guns.centralGun.transform.position, Vector3.zero);
                EnableBulletShot(objectPool.GetPooledObject(), guns.rightGun.transform.position, new Vector3(0, 0, -5));
                guns.leftGunVFX.Play();
                EnableBulletShot(objectPool.GetPooledObject(), guns.leftGun.transform.position, new Vector3(0, 0, 5));
                guns.rightGunVFX.Play();
                break;
            case 4:
                EnableBulletShot(objectPool.GetPooledObject(), guns.centralGun.transform.position, Vector3.zero);
                EnableBulletShot(objectPool.GetPooledObject(), guns.rightGun.transform.position, new Vector3(0, 0, -5));
                guns.leftGunVFX.Play();
                EnableBulletShot(objectPool.GetPooledObject(), guns.leftGun.transform.position, new Vector3(0, 0, 5));
                guns.rightGunVFX.Play();
                EnableBulletShot(objectPool.GetPooledObject(), guns.leftGun.transform.position, new Vector3(0, 0, 15));
                EnableBulletShot(objectPool.GetPooledObject(), guns.rightGun.transform.position, new Vector3(0, 0, -15));
                break;
        }
    }

    void EnableBulletShot(GameObject lazer, Vector3 pos, Vector3 rot) //translating 'pooled' lazer shot to the defined position in the defined rotation
    {       
        if(lazer!=null)
        {
            lazer.transform.SetPositionAndRotation(pos, Quaternion.Euler(rot));
            lazer.SetActive(true);
        }       
    }

    #endregion
}
