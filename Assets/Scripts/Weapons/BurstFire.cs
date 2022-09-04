using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BurstFire : MonoBehaviour
{
    private PlayerInput controls;

    public Bullet bulletPrefab;
    public float fireCooldown; //seconds

    private AudioSource audioData;
    private Animator animator;

    public GameObject spawnPoint;
    public AudioClip fireSFX;

    public int damage = 2;

    public int numberOfShots = 3; //Number of shots in a burst
    public float shotsDelay = 0.1f; //Delay between shots in a burst

    private float delayTimer;
    private int shotCount;

    private float fireTimer; //seconds
    // Start is called before the first frame update
    void Start()
    {
        controls = PlayerInputLoader.Instance.gameObject.GetComponent<PlayerInput>();
        fireTimer = 0f;
        audioData = GetComponent<AudioSource>();
        audioData.Stop();
        animator = GetComponent<Animator>();
        LaserPool.Init(); 

        delayTimer = 0.0f;   
        shotCount = numberOfShots;    
    }

    // Update is called once per frame
    void Update()
    {
        if (controls.actions["Fire"].triggered && fireTimer <=0 && !PauseMenu.gameIsPaused)
        {
            /*AudioSource audio = AudioPool.GetAudioSource();
            audio.clip = fireSFX;
            audio.volume = 0.25f;
            audio.Play(0);
            animator.SetTrigger("Shoot");
            
            Laser laser = LaserPool.GetLaser();
            laser.SetDamage(damage);
            laser.fire(
                spawnPoint.transform.position,
                spawnPoint.transform.forward*1000,
                0.2f,
                gameObject.name
                );*/
            shotCount = numberOfShots;
            delayTimer = 0;
            fireTimer = fireCooldown;
        }

        if (shotCount > 0 && delayTimer <= 0) 
        {
            AudioSource audio = AudioPool.GetAudioSource();
            audio.clip = fireSFX;
            audio.volume = 0.25f;
            audio.Play(0);
            //animator.SetTrigger("Shoot");
            
            Laser laser = LaserPool.GetLaser();
            laser.SetDamage(damage);
            laser.fire(
                spawnPoint.transform.position,
                spawnPoint.transform.forward*1000,
                0.2f,
                gameObject.name
                );
            
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
