using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonFire : MonoBehaviour
{
    private PlayerInput controls;
    private Input playerInputActions; 

    public Camera cam;
    public ExplosiveProjectile explosiveProjectilePrefab;
    public float fireCooldown; //seconds

    private AudioSource audioData;
    private Animator animator;

    public GameObject spawnPoint;

    private float fireTimer; //seconds

    public ParticleSystem explosion;
    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        controls = GetComponent<PlayerInput>();
        playerInputActions = new Input();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Fire.performed += Fire;
        fireTimer = 0f;
        audioData = GetComponent<AudioSource>();
        audioData.Stop();
        animator = GetComponent<Animator>();
        ps = Instantiate(explosion);
        ps.gameObject.AddComponent<Terrain>();
        ps.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        
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
            audioData.Play(0);
            animator.SetTrigger("Shoot");
            ExplosiveProjectile explosiveProjectile = Instantiate(explosiveProjectilePrefab);
            explosiveProjectile.setExplosion(ps);
            explosiveProjectile.transform.parent = spawnPoint.transform;
            explosiveProjectile.transform.localPosition = new Vector3(0,0,0);
            explosiveProjectile.transform.localRotation = Quaternion.Euler(90,0,0);
            explosiveProjectile.transform.parent = null;
            Rigidbody rb = explosiveProjectile.GetComponent<Rigidbody>();
            rb.velocity = (spawnPoint.transform.forward)*explosiveProjectile.projectileVelocity;
            fireTimer = fireCooldown;
        }
    }
}
