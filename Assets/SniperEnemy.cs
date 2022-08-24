using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperEnemy : MonoBehaviour
{
    Transform target;
    Rigidbody rb;
    public float moveSpeed = 0;
    public float maxLookDist = 10;
    public string targetTag = "Player";

    // private LineRenderer laser;
    // private float counter;
    // private float dist;

    // public Transform sniperPos;
    // public Transform playerPos;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
    }


    void Update()
    {
        //Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
    }

    void FixedUpdate() {

        if (target == null || (target.position - transform.position).magnitude > maxLookDist || target.position.z > rb.position.z)
            return;

        Vector3 direction = target.position-transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        rb.MovePosition( rb.position +(target.position - rb.position).normalized*moveSpeed*Time.fixedDeltaTime);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, maxLookDist);
    }


}
