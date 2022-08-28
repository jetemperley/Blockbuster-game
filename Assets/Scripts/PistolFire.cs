using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PistolFire : MonoBehaviour
{
    private PlayerInput controls;
    private Input playerInputActions; 

    public Bullet bulletPrefab;
    public float fireCooldown; //seconds

    private AudioSource audioData;
    private Animator animator;

    public GameObject spawnPoint;
    public AudioClip fireSFX;

    public int damage = 1;

    private float fireTimer; //seconds
    // Start is called before the first frame update
    void Awake()
    {
        controls = GetComponent<PlayerInput>();
        playerInputActions = new Input();
        fireTimer = 0f;
        audioData = GetComponent<AudioSource>();
        audioData.Stop();
        animator = GetComponent<Animator>();
        LaserPool.Init();        
    }

    void OnEnable()
    {
        playerInputActions.Player.Enable();
        playerInputActions.Player.Fire.performed += Fire;
    }

    private void OnDisable()
    {
      playerInputActions.Player.Fire.performed -= Fire;  
      playerInputActions.Player.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer -= Time.deltaTime;        
    }

    public void Fire(InputAction.CallbackContext ctx)
    {
       if (fireTimer <=0 && !PauseMenu.gameIsPaused)
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
        }
    }
    
}
