using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunFire : MonoBehaviour
{
    
    public Bullet bulletPrefab;
    public float fireCooldown; //seconds
    public float fireRadius;

    private Camera cam;
    private AudioSource audioData;
    private Animator animator;
    private bool audioPlaying;

    public GameObject spawnPoint;

    private float fireTimer; //seconds
    LineRenderer line;


    void Start()
    {
        fireTimer = 0f;
        audioData = GetComponent<AudioSource>();
        audioData.Stop();
        animator = GetComponent<Animator>();
        cam = (Camera)FindObjectOfType(typeof(Camera));
        audioPlaying = false;
        audioData.time = 1f;
        line = gameObject.AddComponent<LineRenderer>();
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && fireTimer <= 0)
        {
            if(!audioPlaying){
                audioData.Play(0);
                audioPlaying = !audioPlaying;
            }

            Vector3 dir = cam.transform.forward + new Vector3(Random.Range(fireRadius,-fireRadius),Random.Range(fireRadius,-fireRadius),0);
            Debug.Log(dir);
            Laser laser = LaserPool.GetLaser();
            laser.fire(
                spawnPoint.transform.position,
                dir,
                0.1f
                );
            //audioData.Play(0);
            animator.SetBool("Shooting",true);
            // Bullet bullet = Instantiate(bulletPrefab);
            // bullet.transform.parent = spawnPoint.transform;
            // bullet.transform.localPosition = Vector3.zero;
            // bullet.transform.localRotation = Quaternion.Euler(90, 0, 0);
            // bullet.transform.parent = null;
            // bullet.SetDir(dir);
            fireTimer = fireCooldown;
        }else if(!Input.GetButton("Fire1"))
        {
            audioData.Stop();
            audioPlaying = false;
            animator.SetBool("Shooting",false);
        } else {
            line.enabled = false;
        }

        fireTimer -= Time.deltaTime;
    }
}
