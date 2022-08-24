using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    Transform target;
    Rigidbody rb;
    public float moveSpeed = 0;
    public float maxLookDist = 10;
    public string targetTag = "Player";

    private Vector3[] points;
    LineRenderer laser;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
        laser = GetComponent<LineRenderer>();
        laser.startWidth = 0.1f;
        laser.endWidth = 0.1f;
        points = new Vector3[2];
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, maxLookDist);
    }

    private void LookAtPlayer(){
        if (target == null || (target.position - transform.position).magnitude > maxLookDist || target.position.z > rb.position.z){
            laser.enabled=false;
            return;
        }
        LaserLine();
        Debug.DrawLine(rb.position, target.position,Color.red);
        Vector3 direction = target.position-transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        rb.MovePosition( rb.position +(target.position - rb.position).normalized*moveSpeed*Time.fixedDeltaTime);
    }

    private void LaserLine(){
        laser.enabled=true;
        points[0] = target.position;
        points[1] = rb.position;
        laser.SetPositions(points);
    }
}
