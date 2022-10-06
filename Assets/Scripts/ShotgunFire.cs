using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShotgunFire : MonoBehaviour
{
    private PlayerInput controls;

    public float fireCooldown; //seconds
    
    private float fireTimer; //seconds

    public float maxSpread;
    public int bulletRows;
    public int bulletColumns;

    private AudioSource audioData;
    private Animator animator;
    private Camera cam;

    public GameObject spawnPoint;
    public AudioClip fireSFX;

    public int damage = 1;


    // Start is called before the first frame update
    void Start()
    {
        controls = PlayerInputLoader.Instance.gameObject.GetComponent<PlayerInput>();
        fireTimer = 0f;
        cam = (Camera)FindObjectOfType(typeof(Camera));
        // audioData = GetComponent<AudioSource>();
        // audioData.Stop();
        // animator = GetComponent<Animator>();      
    }

    // Update is called once per frame
    void Update()
    {
        if (controls.actions["Fire"].triggered && fireTimer <=0 && !PauseMenu.gameIsPaused)
        {
            AudioSource audio = AudioPool.GetAudioSource();
            audio.clip = fireSFX;
            audio.volume = 0.25f;
            audio.Play(0);
            // animator.SetTrigger("Shoot");
            Vector3 hitPos;
            // RaycastHit hit;
            // if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1000)){
            //     hitPos = hit.point;
            // } else {
            //     hitPos = cam.transform.position + cam.transform.forward*1000;
            // }
            for(int i = 0; i<bulletRows; i++)
            {
                for(int k = 0; k<bulletColumns; k++)
                {
                    //Vector3 hitDir = (hitPos - spawnPoint.transform.position).normalized; 
                    hitPos = spawnPoint.transform.position + spawnPoint.transform.forward*1000;
                    Vector3 dir = hitPos
                        + new Vector3(
                            Random.Range(-maxSpread,maxSpread), //* Random.Range(maxSpread/2,maxSpread),
                            Random.Range(-maxSpread,maxSpread),//* Random.Range(maxSpread/2,maxSpread),
                            Random.Range(-maxSpread,maxSpread)); //* Random.Range(maxSpread/2,maxSpread));
                    dir = (dir - spawnPoint.transform.position).normalized;
                    Debug.Log(dir);
                    Laser laser = LaserPool.GetLaser();
                    laser.SetDamage(damage);
                    laser.fire(
                        spawnPoint.transform.position,
                        dir,
                        0.2f,
                        gameObject.name
                        );  
                }
                
            
            }
            
            fireTimer = fireCooldown;
        }else{
           fireTimer -= Time.deltaTime; 
        }
                
    }
    
}
