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
    LineRenderer line;

    private float fireTimer; //seconds
    // Start is called before the first frame update
    void Start()
    {
        fireTimer = 0f;
        audioData = GetComponent<AudioSource>();
        audioData.Stop();
        animator = GetComponent<Animator>();
         line = gameObject.AddComponent<LineRenderer>();
         line.startWidth = 0.1f;
         line.endWidth = 0.1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        line.enabled = false;
        if (Input.GetButtonDown("Fire1")&& fireTimer <=0)
        {
            line.enabled = true;
            audioData.Play(0);
            animator.SetTrigger("Shoot");
            RaycastHit hit;

            if (Physics.Raycast (spawnPoint.transform.position, spawnPoint.transform.forward, out hit, 1000)){
                try{
                    Health health = hit.collider.attachedRigidbody.gameObject.GetComponent<Health>();
                    if (health != null){
                    health.takeDamage(1);
                    }
                } catch{

                }
                Vector3[] arr = {spawnPoint.transform.position, hit.point};
               line.SetPositions(arr);
            }else{
                Vector3[] arr = {spawnPoint.transform.position,spawnPoint.transform.forward*1000};
                line.SetPositions(arr);
                // Debug.DrawLine(spawnPoint.transform.position, spawnPoint.transform.position*1000);
            }
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.transform.parent = spawnPoint.transform;
            bullet.transform.localPosition = new Vector3(0,0,0);
            bullet.transform.localRotation = Quaternion.Euler(90,0,0);
            bullet.transform.parent = null;
            bullet.SetDir(spawnPoint.transform.forward);
            fireTimer = fireCooldown;
        } else if (fireTimer > 0){
             line.enabled = true;
        }

        fireTimer -= Time.deltaTime;
    }
}
