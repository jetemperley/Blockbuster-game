using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFire : MonoBehaviour
{
    public Camera cam;
    public ExplosiveProjectile explosiveProjectilePrefab;
    public float fireCooldown; //seconds

    private AudioSource audioData;
    private Animator animator;

    public GameObject spawnPoint;

    private float fireTimer; //seconds
    // Start is called before the first frame update
    void Start()
    {
        fireTimer = 0f;
        //audioData = GetComponent<AudioSource>();
        //audioData.Stop();
       // animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")&& fireTimer <=0)
        {
//            audioData.Play(0);
//            animator.SetTrigger("Shoot");
            ExplosiveProjectile explosiveProjectile = Instantiate(explosiveProjectilePrefab);
            explosiveProjectile.transform.parent = spawnPoint.transform;
            explosiveProjectile.transform.localPosition = new Vector3(0,0,0);
            explosiveProjectile.transform.localRotation = Quaternion.Euler(90,0,0);
            explosiveProjectile.transform.parent = null;
            Rigidbody rb = explosiveProjectile.GetComponent<Rigidbody>();
            rb.velocity = (cam.transform.forward+explosiveProjectile.offset)*explosiveProjectile.projectileVelocity;
            fireTimer = fireCooldown;
        }

        fireTimer -= Time.deltaTime;
    }
}
