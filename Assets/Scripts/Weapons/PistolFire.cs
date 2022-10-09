using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PistolFire : MonoBehaviour
{
    private PlayerInput controls;

    public float fireCooldown; //seconds

    public bool autoFire; //Whether or not the weapon has automatic fire

    private Animator animator;

    public GameObject spawnPoint;
    public AudioClip fireSFX;

    public int damage = 1;

    public bool piercing; //Whether or not the weapon has piercing shots
    public int pierceNum;

    private float fireTimer; //seconds
    // Start is called before the first frame update
    void Start()
    {
        controls = PlayerInputLoader.Instance.gameObject.GetComponent<PlayerInput>();
        fireTimer = 0f;
        animator = GetComponent<Animator>();
        LaserPool.Init();        
    }

    // Update is called once per frame
    void Update()
    {
        if (((!autoFire && controls.actions["Fire"].triggered) | (autoFire && controls.actions["Fire"].ReadValue<float>() == 1)) && fireTimer <=0 && !PauseMenu.gameIsPaused)
        {
            AudioSource audio = AudioPool.GetAudioSource();
            audio.clip = fireSFX;
            audio.volume = PlayerPrefs.GetFloat("sfxSound",1f) * PlayerPrefs.GetFloat("masterSound",1f);
            audio.Play(0);
            animator.SetTrigger("Shoot");
            
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
            
            fireTimer = fireCooldown;
        }else{
           fireTimer -= Time.deltaTime; 
        }
                
    }
    
}
