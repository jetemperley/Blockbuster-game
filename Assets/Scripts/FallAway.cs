using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAway : MonoBehaviour
{
    static Vector3 fallSpeed = new Vector3(0, -1, 0);

    public float backWall = -10;

    private bool fall = false;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (Conductor.getConductor().getTotalSpeed() != 0 && !fall && transform.position.z < Conductor.conductor.getPosition()){
            
            if (rb == null){
                rb = gameObject.AddComponent<Rigidbody>();
            }
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.mass = 1000;
            // rb.drag = 0.1f;
            rb.angularDrag = 1;
            fall = true;
            
            // rb.AddForce(randX, randY, randZ, ForceMode.VelocityChange);
            rb.AddForce(
                Random.Range(-1f, 1f), 
                1, // Random.Range(0f, 1f), 
                0, 
                ForceMode.VelocityChange);
            // rb.MovePosition(rb.position + fallSpeed*Time.deltaTime);
            
        }
        if (transform.position.y < -100)
            Destroy(gameObject);
    }

}

