using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehaviour2 : MonoBehaviour
{
    Transform target;
    Rigidbody rb;
    public float moveSpeed;
    public float maxLookDist;
    public float maxBeepDist;
    public string targetTag = "Player";
    private bool foundTarget = false;
    private Vector3 foundTheMotherFucker;

    private float distance;

    AudioSource mineBeep;
    public AudioClip clip;
    private float beepRate;
    private float time;
    private float volume;

    public Health health;
    private int damage = 1000;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
        foundTarget = false;
        mineBeep = GetComponent<AudioSource>();
        mineBeep.maxDistance = maxLookDist;
        
    }

    // Update is called once per frame
    void Update()
    {

        //distance between player and this gameObject
        distance = (target.position - transform.position).magnitude;
        beepRate += Time.deltaTime;
        
        if(mineBeep != null || clip != null){
            volume=(-1/maxBeepDist)*distance+1f; //y = m*x+b
            if(distance<maxBeepDist){
                if(beepRate>distance/25){
                    mineBeep.Play();
                    beepRate=0;
                }
            }
        }

        if(distance<=maxLookDist){
            if (health != null){
                health.takeDamage(damage);
            }
        }

    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, maxLookDist);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxBeepDist);
        
    }
}



