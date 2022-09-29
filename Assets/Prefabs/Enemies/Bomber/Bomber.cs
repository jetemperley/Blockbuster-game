using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{

    Transform target;
    Rigidbody rb;

    public float moveSpeed = 0;
    public float maxLookDist = 10;
    public string targetTag = "Player";

    public GameObject bomb;
    public float bombSpawnDist = 1;
    public float bombSpeed = 5;

    public float journeyTime = 1.0f;
    float startTime;
    Vector3 centerPoint;
    Vector3 startRelCenter;
    Vector3 endRelCenter;

    public float bombCD = 4;
    private float bombCDTimer;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
        bombCDTimer = 4;
    }

    void Update()
    {
        if(isInRange() && bombCDTimer<bombCD){
            LookAtPlayer();
            bombCDTimer += Time.deltaTime;
        }else if(isInRange() && bombCDTimer>bombCD){
            Shoot();
            bombCDTimer = 0;
        }else{
            //bombCDTimer = 0;
        } 
    }

    private void LookAtPlayer(){
            Vector3 direction = target.position-transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
            rb.MovePosition( rb.position +(target.position - rb.position).normalized*moveSpeed*Time.fixedDeltaTime);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, maxLookDist);
    }

    private bool isInRange(){
        if(target == null || (target.position - transform.position).magnitude > maxLookDist || target.position.z > rb.position.z){
            return false;
        }else{
            return true;
        }
    }

    private void Shoot(){
            GameObject g = Instantiate(bomb);
            g.transform.SetParent(transform);
            g.GetComponent<bomb>().maxSoundDist = maxLookDist*1.5f;
            Vector3 dir = (target.position - transform.position).normalized * bombSpawnDist;
            g.transform.position = transform.position + dir;
    }
}
