using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RailgunFire : MonoBehaviour
{
    private PlayerInput controls; 

    public float fireCooldown; //seconds
    public float beamWidth;

    private AudioSource audioData;
    private Animator animator;

    public GameObject spawnPoint;

    public int damage = 1;

    public bool autoFire;

    private float fireTimer; //seconds
    // Start is called before the first frame update
    void Start()
    {
        controls = PlayerInputLoader.Instance.gameObject.GetComponent<PlayerInput>();
        fireTimer = 0f;
        audioData = GetComponent<AudioSource>();
        audioData.Stop();
        //animator = GetComponent<Animator>();
        LaserPool.Init();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (((!autoFire && controls.actions["Fire"].triggered) | (autoFire && controls.actions["Fire"].ReadValue<float>() == 1)) && fireTimer <=0 && !PauseMenu.gameIsPaused)
        {
            
            audioData.Play(0);
            //animator.SetTrigger("Shoot");
            Health health;

            foreach(RaycastHit hit in Physics.SphereCastAll(spawnPoint.transform.position, beamWidth, spawnPoint.transform.forward*1000, 300.0f))
            {
                try{
                    health = hit.collider.attachedRigidbody.gameObject.GetComponent<Health>();
                    if (health != null && hit.collider.attachedRigidbody.gameObject.layer != 9){
                        health.takeDamage(damage); 
                        /*if (health.getHealth() <= 0){
                            Debug.Log(weap);
                            PlayerStats.getInst().addStat(weap);
                        }*/
                    }
                } catch{}
            }        
            fireTimer = fireCooldown;
        }else{
        fireTimer -= Time.deltaTime;
        }
    }

    public void Fire(InputAction.CallbackContext ctx)
    {
        
    }
}
