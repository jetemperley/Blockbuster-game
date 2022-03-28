using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolFire : MonoBehaviour
{
    public Camera cam;
    public Bullet bulletPrefab;
    public float fireCooldown; //seconds

    private AudioSource audioData;
    private Animator animator;

    public GameObject spawnPoint;

    private float fireTimer; //seconds
    // Start is called before the first frame update
    void Start()
    {
        fireTimer = 0f;
        audioData = GetComponent<AudioSource>();
        audioData.Stop();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")&& fireTimer <=0)
        {
            audioData.Play(0);
            animator.SetBool("Shooting", true);
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.transform.parent = spawnPoint.transform;
            bullet.transform.localPosition = new Vector3(0,0,0);
            bullet.transform.localRotation = Quaternion.Euler(90,0,0);
            bullet.transform.parent = null;
            bullet.SetDir(cam.transform.forward);
            fireTimer = fireCooldown;
        }
        else
        {
            animator.SetBool("Shooting", false);
        }

        fireTimer -= Time.deltaTime;
    }
}
