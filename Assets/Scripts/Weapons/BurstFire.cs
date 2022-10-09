using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BurstFire : MonoBehaviour
{
    private PlayerInput controls;

    public Bullet bulletPrefab;
    public float fireCooldown; //seconds

    private Animator animator;

    public GameObject spawnPoint;
    public AudioClip fireSFX;

    public int damage = 2;

    public int numberOfShots = 3; //Number of shots in a burst
    public float shotsDelay = 0.1f; //Delay between shots in a burst

    public bool piercing;
    public int pierceNum;

    private float delayTimer;
    private int shotCount;

    private float fireTimer; //seconds
    // Start is called before the first frame update
    void Start()
    {
        controls = PlayerInputLoader.Instance.gameObject.GetComponent<PlayerInput>();
        fireTimer = 0f;
        //animator = GetComponent<Animator>();
        LaserPool.Init(); 

        delayTimer = 0.0f;   
        shotCount = 0;    
    }

    // Update is called once per frame
    void Update()
    {
        if (controls.actions["Fire"].triggered && fireTimer <=0 && !PauseMenu.gameIsPaused)
        {
            shotCount = numberOfShots;
            delayTimer = 0;
            fireTimer = fireCooldown;
        }

        if (shotCount > 0 && delayTimer <= 0) 
        {
            AudioSource audio = AudioPool.GetAudioSource();
            audio.clip = fireSFX;
            audio.volume = PlayerPrefs.GetFloat("sfxSound",1f) * PlayerPrefs.GetFloat("masterSound",1f);
            audio.Play(0);
            //animator.SetTrigger("Shoot");
            
            Laser laser = LaserPool.GetLaser();
            laser.SetDamage(damage);
            if (!piercing)
            {
                laser.fire(
                    spawnPoint.transform.position,
                    spawnPoint.transform.forward*1000,
                    0.2f,
                    gameObject.name
                );
            }
            else
            {
                laser.firePierce(
                    spawnPoint.transform.position,
                    spawnPoint.transform.forward*1000,
                    0.2f,
                    150,
                    gameObject.name,
                    pierceNum
                );
            }
            
            shotCount -= 1;
            delayTimer = shotsDelay;
        }    

        if (shotCount <= 0 && fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
        }

        if (shotCount > 0 && delayTimer > 0)
        {
            delayTimer -= Time.deltaTime;
        }
    }
    
}

