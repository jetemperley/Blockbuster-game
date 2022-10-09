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
    public LineRenderer lineRenderer;
    public ParticleSystem particle;

    public GameObject spawnPoint;

    public int damage = 1;

    public bool autoFire;

    private float fireTimer; //seconds
        
    private bool shootDelay;
    private float shootDelayTimer;
    public float shootDelayTime;

    private float laserLineVisible;
    public float laserLineVisibleTimer;
    private Vector3[] linePoints;

    // Start is called before the first frame update
    void Start()
    {
        controls = PlayerInputLoader.Instance.gameObject.GetComponent<PlayerInput>();
        fireTimer = 0f;
        audioData = GetComponent<AudioSource>();
        audioData.Stop();
        //animator = GetComponent<Animator>();
        LaserPool.Init();
        linePoints = new Vector3[2];
        lineRenderer.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (((!autoFire && controls.actions["Fire"].triggered) | (autoFire && controls.actions["Fire"].ReadValue<float>() == 1)) && fireTimer <=0 && !PauseMenu.gameIsPaused)
        {
            audioData.volume = PlayerPrefs.GetFloat("sfxSound",1f) * PlayerPrefs.GetFloat("masterSound",1f);
            audioData.Play(0);
            particle.Play();
            shootDelay=true;
        }
        if(shootDelay == true){ //start timer
            shootDelayTimer+=Time.deltaTime;
        }
        if(shootDelayTimer>=shootDelayTime){ //shoot after timer 
            Debug.Log("Line renderer is on!");
            lineRenderer.enabled = true;
            Shoot();
            shootDelay=false;
            shootDelayTimer=0;
        }
        if(fireTimer>=0){ //countdown fire cooldown
            fireTimer -= Time.deltaTime;
        }
        if(lineRenderer.enabled == true){
            laserLineVisible+= Time.deltaTime;
            if(laserLineVisible>laserLineVisibleTimer){
                Debug.Log("Line renderer is off!");
                lineRenderer.enabled=false;
                laserLineVisible=0;
            }
        }
        //Debug.Log(laserLineVisible);
        linePoints[0] = spawnPoint.transform.position; 
        linePoints[1] = spawnPoint.transform.position+(spawnPoint.transform.forward*300.0f);
        lineRenderer.SetPositions(linePoints);

    }

    public void Shoot(){
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
        }
    

    public void Fire(InputAction.CallbackContext ctx)
    {
        
    }
}
