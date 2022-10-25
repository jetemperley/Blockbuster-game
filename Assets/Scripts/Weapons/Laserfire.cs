using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Laserfire : MonoBehaviour
{
    private PlayerInput controls;
    public float fireCooldown; //seconds
    public int damage = 1;
    public float beamWidth;
    public int laserRange;
    public LineRenderer lineRenderer;
    private Vector3[] points;

    public GameObject spawnPoint;
    public GameObject particle;
    private float ticRate;
    public float ticRateCap = 0.2f;

    public float endWidth;
    public float startWidth;
    public Vector3 laserOffset;

    public AudioClip lasersfx;
    new private AudioSource audio;
    private bool isFiring;



    // Start is called before the first frame update
    void Start()
    {
        controls = PlayerInputLoader.Instance.gameObject.GetComponent<PlayerInput>();     
        lineRenderer = GetComponent<LineRenderer>();
        audio = GetComponent<AudioSource>();
        points = new Vector3[2];
    }

    // Update is called once per frame
    void Update(){
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
        if (controls.actions["Fire"].ReadValue<float>() == 1 && !PauseMenu.gameIsPaused)
        {
            Shoot();
            PlaySound();
            particle.SetActive(true);
        }else{
            StopSound();
            lineRenderer.enabled = false;
            ticRate = 0;
            particle.SetActive(false);
        }
    }

    private void Shoot(){
        lineRenderer.enabled=true;
        foreach(RaycastHit hit in Physics.SphereCastAll(spawnPoint.transform.position, beamWidth, spawnPoint.transform.forward*1000, laserRange))
        {
            /*try{
                if(hit.collider.attachedRigidbody.gameObject.layer == 11){
                    ticRate+=Time.deltaTime;
                    if(ticRate>=ticRateCap){
                        Health health = hit.collider.attachedRigidbody.gameObject.GetComponent<Health>();
                        if (health != null){
                            health.takeDamage(damage); 
                            ticRate=0;
                        }
                    }
                }
            } catch{}*/

            try{
                Health health = hit.collider.attachedRigidbody.gameObject.GetComponent<Health>();
                ticRate+=Time.deltaTime;
                if (ticRate>=ticRateCap && health != null && hit.collider.attachedRigidbody.gameObject.layer != 9){
                    health.takeDamage(damage); 
                    ticRate=0;
                }
            } catch{}
            }
            points[0] = spawnPoint.transform.position + laserOffset; 
            points[1] = spawnPoint.transform.position+(spawnPoint.transform.forward*laserRange);
            lineRenderer.SetPositions(points);
        }

    private void PlaySound()
    {
        if(!audio.isPlaying)
        {
            audio.clip = lasersfx;
            audio.volume = PlayerPrefs.GetFloat("sfxSound",1f) * PlayerPrefs.GetFloat("masterSound",1f);
            audio.Play(0); 
        }
        if(audio.volume != PlayerPrefs.GetFloat("sfxSound",1f) * PlayerPrefs.GetFloat("masterSound",1f))
        {
            audio.volume = PlayerPrefs.GetFloat("sfxSound",1f) * PlayerPrefs.GetFloat("masterSound",1f);
        }
    }

    private void StopSound()
    {
        if(audio.isPlaying)
        {
            audio.volume -= Time.deltaTime;
            if(audio.volume <= 0)
            {
                audio.Stop();
            }
        }
    }
    
}

    
        

