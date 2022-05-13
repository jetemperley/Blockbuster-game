using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunFire : MonoBehaviour
{
    public float fireCooldown; //seconds
    public float beamWidth;

    private AudioSource audioData;
    private Animator animator;

    public GameObject spawnPoint;

    public int damage = 1;

    private float fireTimer; //seconds
    // Start is called before the first frame update
    void Start()
    {
        fireTimer = 0f;
        audioData = GetComponent<AudioSource>();
        audioData.Stop();
        //animator = GetComponent<Animator>();
        LaserPool.Init();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")&& fireTimer <=0)
        {
            
            audioData.Play(0);
            //animator.SetTrigger("Shoot");
            
            /*Laser laser = LaserPool.GetLaser();
            laser.SetDamage(damage);
            laser.fire(
                spawnPoint.transform.position,
                spawnPoint.transform.forward*1000,
                0.2f,
                gameObject.name
                );*/

            Health health;

            foreach(RaycastHit hit in Physics.SphereCastAll(spawnPoint.transform.position, beamWidth, spawnPoint.transform.forward*1000, 300.0f))
            {
                try{
                    health = hit.collider.attachedRigidbody.gameObject.GetComponent<Health>();
                    if (health != null){
                        health.takeDamage(damage); 
                        /*if (health.getHealth() <= 0){
                            Debug.Log(weap);
                            PlayerStats.getInst().addStat(weap);
                        }*/
                    }
                } catch{}
            }
        
            fireTimer = fireCooldown;
        } else if (fireTimer > 0){
            fireTimer -= Time.deltaTime;
        }

        
    }
}
