using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolFire : MonoBehaviour
{
    public Bullet bulletPrefab;
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
        fireTimer = 0f;
        audioData = GetComponent<AudioSource>();
        audioData.Stop();
        animator = GetComponent<Animator>();
        LaserPool.Init();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")&& fireTimer <=0)
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
            
            // Bullet bullet = Instantiate(bulletPrefab);
            // bullet.transform.parent = spawnPoint.transform;
            // bullet.transform.localPosition = new Vector3(0,0,0);
            // bullet.transform.localRotation = Quaternion.Euler(90,0,0);
            // bullet.transform.parent = null;
            // bullet.SetDir(spawnPoint.transform.forward);
            fireTimer = fireCooldown;
        } else if (fireTimer > 0){
            fireTimer -= Time.deltaTime;
        }

        
    }
}
