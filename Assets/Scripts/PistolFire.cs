using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PistolFire : MonoBehaviour
{
    private PlayerInput controls;

    public float fireCooldown; //seconds

    private AudioSource audioData;
    private Animator animator;

    public GameObject spawnPoint;
    public AudioClip fireSFX;

    public int damage = 1;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (controls.actions["Fire"].triggered && fireTimer <=0 && !PauseMenu.gameIsPaused)
        {
            AudioSource audio = AudioPool.GetAudioSource();
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
                );
            
            fireTimer = fireCooldown;
        }else{
           fireTimer -= Time.deltaTime; 
        }
                
    }
    
}
