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

    public bool autoFire;
    public bool piercing;
    public int pierceNum;

    // Start is called before the first frame update
    void Start()
    {
        controls = PlayerInputLoader.Instance.gameObject.GetComponent<PlayerInput>();
        fireTimer = 0f;
        cam = (Camera)FindObjectOfType(typeof(Camera));
        animator = GetComponent<Animator>();  
        Debug.Log(animator);    
    }

    // Update is called once per frame
    void Update()
    {
        if (((!autoFire && controls.actions["Fire"].triggered) | (autoFire && controls.actions["Fire"].ReadValue<float>() == 1)) && fireTimer <=0 && !PauseMenu.gameIsPaused)
        {
            AudioSource audio = AudioPool.GetAudioSource();
            audio.clip = fireSFX;
            audio.volume = PlayerPrefs.GetFloat("sfxSound",1f) * PlayerPrefs.GetFloat("masterSound",1f);
            audio.Play(0);
            animator.SetTrigger("Shoot");
            Vector3 hitPos;
            for(int i = 0; i<bulletRows; i++)
            {
                for(int k = 0; k<bulletColumns; k++)
                {
                     
                    hitPos = spawnPoint.transform.position + spawnPoint.transform.forward*1000;
                    Vector3 dir = hitPos
                        + new Vector3(
                            Random.Range(-maxSpread,maxSpread), //* Random.Range(maxSpread/2,maxSpread),
                            Random.Range(-maxSpread,maxSpread),//* Random.Range(maxSpread/2,maxSpread),
                            Random.Range(-maxSpread,maxSpread)); //* Random.Range(maxSpread/2,maxSpread));
                    dir = (dir - spawnPoint.transform.position).normalized;
                    Laser laser = LaserPool.GetLaser();
                    laser.SetDamage(damage);
                    if (!piercing)
                    {
                        laser.fire(
                            spawnPoint.transform.position,
                            dir,
                            0.2f,
                            gameObject.name
                            ); 
                    } 
                    else
                    {
                        laser.firePierce(
                            spawnPoint.transform.position,
                            dir,
                            0.2f,
                            150,
                            gameObject.name,
                            pierceNum
                        ); 
                    }
                }
                
            
            }
            
            fireTimer = fireCooldown;
        }else{
           fireTimer -= Time.deltaTime; 
        }
                
    }
    
}
