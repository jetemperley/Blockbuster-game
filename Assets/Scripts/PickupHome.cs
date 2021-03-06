using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHome : MonoBehaviour
{
    Transform target;
    Rigidbody rb;
    public float moveSpeed = 10;
    public float maxLookDist = 5;
    public string targetTag = "Player";
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {

        if (target == null || (target.position - transform.position).magnitude > maxLookDist)
            return;

        
        rb.MovePosition( rb.position +
            (target.position - rb.position).normalized*moveSpeed*Time.fixedDeltaTime);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, maxLookDist);
    }
}
