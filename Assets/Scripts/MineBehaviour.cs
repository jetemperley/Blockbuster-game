using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehaviour : MonoBehaviour
{
    Transform target;
    Rigidbody rb;
    public float moveSpeed;
    public float maxLookDist;
    public string targetTag = "Player";
    private bool foundTarget = false;
    private Vector3 foundTheMotherFucker;

    private float distance;

    public AudioSource mineBeep;
    public AudioClip clip;
    private float beepRate;
    private float time;
    private float volume;

    private FieldOfView FoV;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        FoV = GetComponent<FieldOfView>();
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
        foundTarget = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        //distance between player and this gameObject
        distance = (target.position - transform.position).magnitude;
        beepRate += Time.deltaTime;

        if(isInRange() && foundTarget==false){
            LookAtPlayer();
        }
        if(foundTarget==true){
        transform.Translate(0,0,moveSpeed*Time.deltaTime);
        volume= PlayerPrefs.GetFloat("sfxSound",1f) * PlayerPrefs.GetFloat("masterSound",1f)
                *(-1/maxLookDist)*distance+0.5f; //y = m*x+b
        if(target.position.z < rb.position.z+7){
            if(beepRate>distance/50){
                mineBeep.PlayOneShot(clip, volume);
                beepRate=0;
            }
        }
        }
    }

    private void LookAtPlayer(){
            Vector3 direction = target.position-transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
            rb.MovePosition( rb.position +(target.position - rb.position).normalized*moveSpeed*Time.fixedDeltaTime);
            foundTarget = true;
    }

    private bool isInRange(){
        //if(target == null || (target.position - transform.position).magnitude > maxLookDist || target.position.z > rb.position.z){
        if(FoV.visibleTargets.Count <= 0){  
            Debug.Log("I'm not in range");
            return false;
        }else{
            Debug.Log("i'm in range!");
            return true;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, maxLookDist);
        
    }
}



