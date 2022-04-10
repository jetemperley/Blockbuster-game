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

    public GameObject spawnPoint;

    private float fireTimer; //seconds
    // Start is called before the first frame update
    void Start()
    {
        fireTimer = 0f;
        //audioData = GetComponent<AudioSource>();
        //audioData.Stop();
        animator = GetComponent<Animator>();
        cam = (Camera)FindObjectOfType(typeof(Camera));

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && fireTimer <= 0)
        {
            //audioData.Play(0);
            animator.SetTrigger("Shoot");
            animator.SetBool("Shooting",true);
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.transform.parent = spawnPoint.transform;
            bullet.transform.localPosition = Vector3.zero;
            bullet.transform.localRotation = Quaternion.Euler(90, 0, 0);
            bullet.transform.parent = null;
            bullet.SetDir(cam.transform.forward + new Vector3(Random.Range(fireRadius,-fireRadius),Random.Range(fireRadius,-fireRadius),0));
            fireTimer = fireCooldown;
        }else if(!Input.GetButton("Fire1"))
        {
            animator.SetBool("Shooting",false);
        }

        fireTimer -= Time.deltaTime;
    }
}
