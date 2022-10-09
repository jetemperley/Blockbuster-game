using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonFire : MonoBehaviour
{
    private PlayerInput controls;

    public Camera cam;
    public ExplosiveProjectile explosiveProjectilePrefab;
    public float fireCooldown; //seconds

    public float projectileVelocity;
    public float explosiveRadius;
    public int damage;

    public bool autoFire;

    public AudioClip clip;

    private Animator animator;

    public GameObject spawnPoint;

    private float fireTimer; //seconds

    public ParticleSystem explosion;
    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        controls = PlayerInputLoader.Instance.gameObject.GetComponent<PlayerInput>();
        fireTimer = 0f;
        animator = GetComponent<Animator>();
        ps = Instantiate(explosion);
        ps.gameObject.AddComponent<Terrain>();
        ps.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (((!autoFire && controls.actions["Fire"].triggered) | (autoFire && controls.actions["Fire"].ReadValue<float>() == 1)) && fireTimer <=0 && !PauseMenu.gameIsPaused)
        {
            // AudioSource audio = AudioPool.GetAudioSource();
            // audio.clip = clip;
            // audio.volume = PlayerPrefs.GetFloat("sfxSound",1f) * PlayerPrefs.GetFloat("masterSound",1f);
            // audio.Play(0);
            animator.SetTrigger("Shoot");
            ExplosiveProjectile explosiveProjectile = Instantiate(explosiveProjectilePrefab);
            explosiveProjectile.SetProperties(projectileVelocity, explosiveRadius, damage);
            explosiveProjectile.setExplosion(ps);
            explosiveProjectile.transform.parent = spawnPoint.transform;
            explosiveProjectile.transform.localPosition = new Vector3(0,0,0);
            explosiveProjectile.transform.localRotation = Quaternion.Euler(90,0,0);
            explosiveProjectile.transform.parent = null;
            Rigidbody rb = explosiveProjectile.GetComponent<Rigidbody>();
            rb.velocity = (spawnPoint.transform.forward)*explosiveProjectile.projectileVelocity;
            fireTimer = fireCooldown;
        }else{
           fireTimer -= Time.deltaTime; 
        }
        
    }

}
