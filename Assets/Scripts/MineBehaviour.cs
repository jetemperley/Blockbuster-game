using System.Collections;
using System;
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



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        try {
            target = GameObject.FindGameObjectWithTag(targetTag).transform;
        } catch (Exception e){
            Destroy(this);
        }
        foundTarget = false;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //distance between player and this gameObject
        distance = (target.position - transform.position).magnitude;
        beepRate += Time.deltaTime;

        if (target == null || distance > maxLookDist)
            return;



        if(foundTarget==false){
            Vector3 direction = target.position-transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
            foundTarget = true;
        }
        transform.Translate(0,0,moveSpeed*Time.deltaTime);

        volume=(-1/maxLookDist)*distance+1; //y = m*x+b
        if(target.position.z < rb.position.z+7){
            if(beepRate>distance/50){
                mineBeep.PlayOneShot(clip, volume);
                beepRate=0;
            }
        }

    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, maxLookDist);
        
    }
}



