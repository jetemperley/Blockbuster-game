using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehaviour : MonoBehaviour
{
    Transform target;
    Rigidbody rb;
    public float moveSpeed = 3;
    public float maxLookDist = 10;
    public string targetTag = "Player";
    private bool foundTarget = false;
    private Vector3 foundTheMotherFucker;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
        foundTarget = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (target == null || (target.position - transform.position).magnitude > maxLookDist)
            return;



        if(foundTarget==false){
            Vector3 direction = target.position-transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
            foundTarget = true;
        }
        transform.Translate(0,0,moveSpeed*Time.deltaTime);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, maxLookDist);
        
    }
}



